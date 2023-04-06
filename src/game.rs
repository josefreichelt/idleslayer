#![allow(dead_code, unused_variables, unused_imports)]
use std::{
    io::{self, Stdout},
    time::{Duration, Instant},
};

use crossterm::event::{self, Event, KeyCode};
use ratatui::{
    backend::{Backend, CrosstermBackend},
    layout::{Alignment, Constraint, Layout},
    style::{Color, Style},
    widgets::{Block, Borders, Gauge, Paragraph},
    Frame, Terminal,
};

use crate::{enemy::Enemy, game_state, player::Player};
const TICK_RATE: Duration = Duration::from_millis(50);

pub fn game_loop(terminal: &mut Terminal<CrosstermBackend<Stdout>>) -> Result<(), io::Error> {
    let mut game_state = game_state::GameState {
        player: Player::new(),
        enemy: Enemy::new(),
        should_quit: false,
        menu_state: game_state::MenuState::Battle,
    };

    let mut last_tick = Instant::now();
    let draw_tick_rate = TICK_RATE;

    loop {
        if crossterm::event::poll(draw_tick_rate).unwrap() {
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

        if last_tick.elapsed() >= draw_tick_rate {
            terminal.draw(|f| match game_state.menu_state {
                game_state::MenuState::MainMenu => {
                    todo!("Draw main menu");
                }
                game_state::MenuState::Battle => {
                    draw_battle(f, &mut game_state.player, &mut game_state.enemy);
                }
                game_state::MenuState::Skills => {
                    todo!("Draw skills screen")
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

fn draw_battle<B: Backend>(f: &mut Frame<B>, player: &mut Player, enemy: &mut Enemy) {
    let enemy_health_percentage = calc_damage(player.damage, enemy);
    calc_level(player);

    let size = f.size();
    let layout = Layout::default()
        .constraints([Constraint::Max(10), Constraint::Percentage(50)].as_ref())
        .split(size);
    let player_block = Block::default()
        .title("[ Idle Slayer ]")
        .borders(Borders::ALL)
        .title_alignment(Alignment::Center);
    let player_block_layout = Layout::default()
        .margin(1)
        .constraints([Constraint::Percentage(50), Constraint::Max(3)].as_ref())
        .split(layout[0]);
    let player_paragraph = player.into_paragraph();
    f.render_widget(player_block, layout[0]);
    let player_paragraph = player_paragraph;
    f.render_widget(player_paragraph, player_block_layout[0]);

    let xp_bar = Gauge::default()
        .block(Block::default().title("XP").borders(Borders::ALL))
        .gauge_style(Style::default().fg(Color::Green))
        .label(format!("{}/{}", player.current_lvl_xp, player.next_lvl_xp))
        .percent(((player.current_lvl_xp as f32 / player.next_lvl_xp as f32) * 100.0) as u16);
    f.render_widget(xp_bar, player_block_layout[1]);
    let enemy_block = Block::default()
        .title(format!("[ {} ]", enemy.name))
        .borders(Borders::ALL)
        .title_alignment(Alignment::Center);
    let enemy_layout = Layout::default()
        .constraints([Constraint::Max(5), Constraint::Min(0)])
        .split(layout[1]);
    if enemy_health_percentage == 0 {
        player.current_lvl_xp += enemy.xp;
    }
    let enemy_health = Gauge::default()
        .block(enemy_block)
        .gauge_style(Style::default().fg(Color::Red))
        .percent(enemy_health_percentage);
    let enemy_name = Paragraph::new(enemy.name.clone());

    f.render_widget(enemy_health, enemy_layout[0]);
}

fn calc_damage(damage: u64, enemy: &mut Enemy) -> u16 {
    enemy.health -= damage as i64;
    if enemy.health <= 0 {
        enemy.health = enemy.max_health;
        return 0;
    } else {
        let damaged = (enemy.health as f32 / enemy.max_health as f32 * 100.0) as u16;
        return damaged;
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
