use std::io::{self, Stdout};

use crossterm::{
    execute,
    terminal::{disable_raw_mode, enable_raw_mode, EnterAlternateScreen, LeaveAlternateScreen}, event::{DisableMouseCapture, EnableMouseCapture},
};
use ratatui::{backend::CrosstermBackend, Terminal};

mod enemy;
mod game;
mod game_state;
mod player;

fn main() -> Result<(), io::Error> {
    let mut terminal = setup_terminal()?;
    game::game_loop(&mut terminal)?;
    teardown_terminal(terminal)?;
    return Ok(());
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
