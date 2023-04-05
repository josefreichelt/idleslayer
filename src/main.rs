#![allow(dead_code, unused_variables, unused_imports)]

use crossterm::{
    event::{self, DisableMouseCapture, EnableMouseCapture, Event, KeyCode, KeyModifiers},
    execute,
    terminal::{disable_raw_mode, enable_raw_mode, EnterAlternateScreen, LeaveAlternateScreen},
};
use enemy::Enemy;
use player::Player;
use ratatui::{
    backend::{Backend, CrosstermBackend},
    layout::{Alignment, Constraint, Direction, Layout},
    style::{Color, Modifier, Style},
    text::{Span, Spans},
    widgets::{Block, Borders, Gauge, Paragraph, Widget},
    Frame, Terminal,
};
use std::{
    io::{self, Stdout},
    thread,
    time::{Duration, Instant},
};

mod enemy;
mod player;

fn main() -> Result<(), io::Error> {
    let mut terminal = setup_terminal()?;
    let mut should_quit = false;

    let mut last_tick = Instant::now();
    let draw_tick_rate = Duration::from_millis(250);
    let mut player = Player::new();
    let mut enemy = Enemy::new();
    loop {
        if crossterm::event::poll(draw_tick_rate).unwrap() {
            if let Event::Key(key) = event::read().unwrap() {
                match key.code {
                    KeyCode::Char(c) => {
                        if c == 'q' {
                            should_quit = true;
                        }
                    }
                    _ => {
                        println!("{:?}", key.code)
                    }
                }
            }
        }

        if last_tick.elapsed() >= draw_tick_rate {
            terminal.draw(|f| {
                ui(f, &mut player, &mut enemy);
            })?;
            last_tick = Instant::now();
        }
        if should_quit {
            break;
        }
    }

    teardown_terminal(terminal)?;
    return Ok(());
}

fn ui<B: Backend>(f: &mut Frame<B>, mut player: &Player, enemy: &mut Enemy) {
    let size = f.size();
    let layout = Layout::default()
        .constraints([Constraint::Max(10), Constraint::Percentage(50)].as_ref())
        .split(size);
    let block = Block::default()
        .title("[ Idle Slayer ]")
        .borders(Borders::ALL)
        .title_alignment(Alignment::Center);
    let enemy_block = Block::default()
        .title(format!("[ {} ]",enemy.name))
        .borders(Borders::ALL)
        .title_alignment(Alignment::Center);

    // let player_info = Paragraph::new(Span::styled("Hero info", Style::default().fg(Color::Red)))
    //     .alignment(Alignment::Left)
    //     .block(block);
    let enemy_layout = Layout::default()
        .constraints([Constraint::Max(5), Constraint::Min(0)])
        .split(layout[1]);
    let enemy_health = Gauge::default()
    .block(enemy_block)
    .gauge_style(Style::default().fg(Color::Red))
    .percent(calc_damage(player.damage, enemy));
    let enemy_name = Paragraph::new(enemy.name.clone());
    let player_paragraph = player.into_paragraph();
    let player_paragraph = player_paragraph.block(block);
    f.render_widget(player_paragraph, layout[0]);

    f.render_widget(enemy_health, enemy_layout[0]);
}

fn setup_terminal() -> std::io::Result<Terminal<CrosstermBackend<Stdout>>> {
    // setup terminal
    enable_raw_mode()?;
    let mut stdout = io::stdout();
    execute!(stdout, EnterAlternateScreen, EnableMouseCapture)?;
    let backend = CrosstermBackend::new(stdout);
    Terminal::new(backend)
}

fn teardown_terminal(
    mut terminal: Terminal<CrosstermBackend<Stdout>>,
) -> io::Result<Terminal<CrosstermBackend<Stdout>>> {
    // restore terminal
    disable_raw_mode()?;
    execute!(
        terminal.backend_mut(),
        LeaveAlternateScreen,
        DisableMouseCapture
    )?;
    terminal.show_cursor()?;
    Ok(terminal)
}

fn calc_damage(damage: u64, enemy: &mut Enemy) -> u16 {
    enemy.health -= damage as i64;
    if enemy.health <= 0 {
        enemy.health = enemy.max_health;
        return 0;
    } else { 
        let damaged= (enemy.health as f32 / enemy.max_health as f32 * 100.0) as u16;
        return  damaged;
    }
}
