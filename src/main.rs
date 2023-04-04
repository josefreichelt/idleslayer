#![allow(dead_code, unused_variables, unused_imports)]

use crossterm::{
    event::{self, DisableMouseCapture, EnableMouseCapture, Event, KeyCode},
    execute,
    terminal::{disable_raw_mode, enable_raw_mode, EnterAlternateScreen, LeaveAlternateScreen},
};
use ratatui::{
    backend::{Backend, CrosstermBackend},
    layout::{Alignment, Constraint, Direction, Layout},
    style::{Color, Style},
    text::{Span, Spans},
    widgets::{Block, Borders, Paragraph, Widget, Gauge},
    Frame, Terminal,
};
use std::{io, thread, time::{Duration, Instant}};

fn main() -> Result<(), io::Error> {
    // setup terminal
    enable_raw_mode()?;
    let mut stdout = io::stdout();
    execute!(stdout, EnterAlternateScreen, EnableMouseCapture)?;
    let backend = CrosstermBackend::new(stdout);
    let mut terminal = Terminal::new(backend)?;
    let mut should_quit = false;

    let mut last_tick = Instant::now();
    let draw_tick_rate = Duration::from_millis(250);
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
                ui(f);
            })?;
            last_tick = Instant::now();
        }
        if should_quit {
            break;
        }
    }

    // restore terminal
    disable_raw_mode()?;
    execute!(
        terminal.backend_mut(),
        LeaveAlternateScreen,
        DisableMouseCapture
    )?;
    terminal.show_cursor()?;

    return Ok(());
}

fn ui<B: Backend>(f: &mut Frame<B>) {
    let size = f.size();
    let layout = Layout::default()
        .constraints([Constraint::Max(10), Constraint::Percentage(50)].as_ref())
        .split(size);
    let block = Block::default()
        .title("[ Idle Slayer ]")
        .borders(Borders::ALL)
        .title_alignment(Alignment::Center);
    let enemy_block = Block::default()
        .title("[ ENEMY ]")
        .borders(Borders::ALL)
        .title_alignment(Alignment::Center);
    let player_info = Paragraph::new(Span::styled("Hero info", Style::default().fg(Color::Red)))
        .alignment(Alignment::Left)
        .block(block);
    let enemy_layout = Layout::default().constraints([Constraint::Max(5),Constraint::Min(0)]).split(layout[1]);
    let enemy_health_block = Block::default().borders(Borders::ALL);
    let enemy_health = Gauge::default().block(enemy_block).gauge_style(Style::default().fg(Color::Red)).percent(40);
    f.render_widget(player_info, layout[0]);

    f.render_widget(enemy_health, enemy_layout[0]);
}
