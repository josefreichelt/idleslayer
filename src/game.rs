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
    widgets::{Block, BorderType, Borders, Clear, Gauge, Paragraph},
    Frame, Terminal,
};

use crate::{
    enemy::Enemy,
    game_state::{self, MenuState},
    player::Player,
    render,
};
const TICK_RATE: Duration = Duration::from_millis(50);

pub fn game_loop(terminal: &mut Terminal<CrosstermBackend<Stdout>>) -> Result<(), io::Error> {
    let mut game_state = game_state::GameState {
        player: Player::new(),
        enemy: Enemy::new(),
        should_quit: false,
        menu_state: game_state::MenuState::Battle,
        is_paused: false,
    };

    let mut last_tick = Instant::now();

    loop {
        handle_input(&mut game_state);
        if last_tick.elapsed() >= TICK_RATE {
            terminal.draw(|frame| {
                let size = frame.size();
                let main_layout = Layout::default()
                    .constraints([Constraint::Percentage(95), Constraint::Min(3)].as_ref())
                    .split(size);
                frame.render_widget(Clear, main_layout[0]);

                match game_state.menu_state {
                    game_state::MenuState::MainMenu => {
                        todo!("Draw main menu");
                    }
                    game_state::MenuState::Battle => {
                        if !game_state.is_paused {
                            update_battle(&mut game_state.player, &mut game_state.enemy);
                        }
                        render::battle::draw_battle(
                            frame,
                            &main_layout,
                            &mut game_state.player,
                            &mut game_state.enemy,
                        );
                        draw_keys(frame, &main_layout[1], &game_state)
                    }
                    game_state::MenuState::Skills => {
                        render::skills::draw_skills(
                            frame,
                            main_layout[0],
                            &mut game_state.player,
                            &mut game_state.enemy,
                        );
                        draw_keys(frame, &main_layout[1], &game_state);
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
            if key.kind == event::KeyEventKind::Press {
                match key.code {
                    KeyCode::Char(c) => match c {
                        'q' => {
                            game_state.should_quit = true;
                        }
                        'p' => {
                            game_state.is_paused = !game_state.is_paused;
                        }
                        's' => {
                            game_state.is_paused = true;
                            game_state.menu_state = MenuState::Skills;
                        }
                        'b' => {
                            game_state.is_paused = false;
                            game_state.menu_state = MenuState::Battle;
                        }
                        _ => {}
                    },
                    _ => {
                        println!("{:?}", key.code)
                    }
                }
            }
        }
    }
}

fn update_battle(player: &mut Player, enemy: &mut Enemy) {
    process_damage(player, enemy);
    calc_level(player);
}

fn process_damage(player: &mut Player, enemy: &mut Enemy) {
    enemy.health -= player.damage as i64;
    if enemy.health <= 0 {
        player.current_lvl_xp += enemy.xp;
        player.gold += enemy.gold;
        enemy.health = enemy.max_health;
    }
}

fn calc_level(player: &mut Player) {
    if player.current_lvl_xp >= player.next_lvl_xp {
        player.lvl += 1;
        player.next_lvl_xp = player.next_lvl_xp * 2;
        player.total_xp += player.current_lvl_xp;
        player.current_lvl_xp = 0;
    }
}

fn draw_keys<B: Backend>(frame: &mut Frame<B>, layout: &Rect, game_state: &game_state::GameState) {
    let keys_block = Block::default()
        .title("Available commands")
        .borders(Borders::ALL)
        .border_style(Style::default().fg(Color::Cyan))
        .border_type(BorderType::Rounded);
    match game_state.menu_state {
        MenuState::Battle => {
            let keys = Spans::from(vec![
                Span::styled("q - quit", Style::default().fg(Color::Red)),
                Span::from(" | "),
                Span::styled("s - skills", Style::default().fg(Color::Green)),
                Span::from(" | "),
                Span::styled(
                    if game_state.is_paused {
                        "p - unpause"
                    } else {
                        "p - pause"
                    },
                    Style::default().fg(Color::Green),
                ),
            ]);
            let keys_paragraph = Paragraph::new(keys).block(keys_block);
            frame.render_widget(keys_paragraph, layout.clone());
        }
        MenuState::Skills => {
            let keys = Spans::from(vec![
                Span::styled("q - quit", Style::default().fg(Color::Red)),
                Span::from(" | "),
                Span::styled("b - back", Style::default().fg(Color::Green)),
            ]);
            let keys_paragraph = Paragraph::new(keys).block(keys_block);
            frame.render_widget(keys_paragraph, layout.clone());
        }
        _ => {}
    }
}
