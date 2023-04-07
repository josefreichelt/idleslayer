#![allow(dead_code, unused_variables, unused_imports)]
use std::{
    io::{self, Stdout},
    ops::Index,
    rc::Rc,
    time::{Duration, Instant},
};

use crossterm::event::{self, Event, KeyCode};
use ratatui::{
    backend::{Backend, CrosstermBackend},
    layout::{Alignment, Constraint, Layout, Rect},
    style::{Color, Modifier, Style},
    text::{Span, Spans},
    widgets::{Block, Borders, Gauge, Paragraph, BorderType},
    Frame, Terminal,
};

use crate::{
    enemy::Enemy,
    game_state::{self, MenuState},
    player::Player,
};
const TICK_RATE: Duration = Duration::from_millis(50);

pub fn game_loop(terminal: &mut Terminal<CrosstermBackend<Stdout>>) -> Result<(), io::Error> {
    let mut game_state = game_state::GameState {
        player: Player::new(),
        enemy: Enemy::new(),
        should_quit: false,
        menu_state: game_state::MenuState::Battle,
    };

    let mut last_tick = Instant::now();

    loop {
        handle_input(&mut game_state);
        if last_tick.elapsed() >= TICK_RATE {
            terminal.draw(|frame| {
                let size = frame.size();
                let main_layout = Layout::default()
                    .constraints([Constraint::Percentage(95), Constraint::Min(2)].as_ref())
                    .split(size);
                match game_state.menu_state {
                    game_state::MenuState::MainMenu => {
                        todo!("Draw main menu");
                    }
                    game_state::MenuState::Battle => {
                        update_battle(&mut game_state.player, &mut game_state.enemy);
                        draw_battle(
                            frame,
                            &main_layout,
                            &mut game_state.player,
                            &mut game_state.enemy,
                        );
                        draw_keys(frame, &main_layout[1], &game_state.menu_state)
                    }
                    game_state::MenuState::Skills => {
                        todo!("Draw skills screen")
                    }
                }
            })?;
            last_tick = Instant::now();
        }

        if game_state.should_quit {
            break;
        }
    }
    Ok(())
}

fn handle_input(game_state: &mut game_state::GameState) {
    if crossterm::event::poll(TICK_RATE).unwrap() {
        if let Event::Key(key) = event::read().unwrap() {
            match key.code {
                KeyCode::Char(c) => {
                    if c == 'q' {
                        game_state.should_quit = true;
                    }
                }
                _ => {
                    println!("{:?}", key.code)
                }
            }
        }
    }
}

fn update_battle(player: &mut Player, enemy: &mut Enemy) {
    process_damage(player, enemy);
    calc_level(player);
}

fn draw_battle<B: Backend>(
    frame: &mut Frame<B>,
    main_layout: &Rc<[Rect]>,
    player: &mut Player,
    enemy: &mut Enemy,
) {
    let layout = Layout::default()
        .constraints(
            [
                Constraint::Max(10),
                Constraint::Percentage(50),
                Constraint::Max(5),
            ]
            .as_ref(),
        )
        .split(main_layout[0]);
    let player_block = Block::default()
        .title("[ Idle Slayer ]")
        .borders(Borders::ALL)
        .title_alignment(Alignment::Center);
    let player_block_layout = Layout::default()
        .margin(1)
        .constraints([Constraint::Percentage(50), Constraint::Max(3)].as_ref())
        .split(layout[0]);
    let player_paragraph = player.into_paragraph();
    frame.render_widget(player_block, layout[0]);
    let player_paragraph = player_paragraph;
    frame.render_widget(player_paragraph, player_block_layout[0]);

    let xp_bar = Gauge::default()
        .block(Block::default().title("XP").borders(Borders::ALL))
        .gauge_style(Style::default().fg(Color::Green))
        .label(format!("{}/{}", player.current_lvl_xp, player.next_lvl_xp))
        .percent(((player.current_lvl_xp as f32 / player.next_lvl_xp as f32) * 100.0) as u16);
    frame.render_widget(xp_bar, player_block_layout[1]);
    let enemy_block = Block::default()
        .title(format!("[ {} ]", enemy.name))
        .borders(Borders::ALL)
        .title_alignment(Alignment::Center);
    let enemy_layout = Layout::default()
        .constraints([Constraint::Max(5), Constraint::Min(0)])
        .split(layout[1]);

    let enemy_health = Gauge::default()
        .block(enemy_block)
        .gauge_style(Style::default().fg(Color::Red))
        .percent(enemy.health_percentage());
    let enemy_name = Paragraph::new(enemy.name.clone());

    frame.render_widget(enemy_health, enemy_layout[0]);
}

fn process_damage(player: &mut Player, enemy: &mut Enemy) {
    enemy.health -= player.damage as i64;
    if enemy.health <= 0 {
        player.current_lvl_xp += enemy.xp;
        enemy.health = enemy.max_health;
    }
}

fn calc_level(player: &mut Player) {
    if player.current_lvl_xp >= player.next_lvl_xp {
        player.lvl += 1;
        player.next_lvl_xp = player.next_lvl_xp * 2;
        player.damage += 1;
        player.total_xp += player.current_lvl_xp;
        player.current_lvl_xp = 0;
    }
}

fn draw_keys<B: Backend>(frame: &mut Frame<B>, layout: &Rect, menu_state: &MenuState) {
    let keys_block = Block::default().title("Available commands")
        .borders(Borders::ALL)
        .border_style(Style::default().fg(Color::Cyan)).border_type(BorderType::Rounded);
    match menu_state {
        MenuState::Battle => {
            let keys = Spans::from(vec![
                Span::styled("q - quit", Style::default().fg(Color::Red)),
                Span::from(" | "),
                Span::styled("s - skills", Style::default().fg(Color::Green)),
            ]);
            let keys_paragraph = Paragraph::new(keys).block(keys_block);
            frame.render_widget(keys_paragraph, layout.clone());
        }
        _ => {}
    }
}
