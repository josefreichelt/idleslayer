use std::rc::Rc;

use ratatui::{
    backend::Backend,
    layout::{Alignment, Constraint, Layout, Rect},
    style::{Color, Style},
    widgets::{Block, Borders, Clear, Gauge, Paragraph},
    Frame,
};

use crate::{enemy::Enemy, player::Player};

pub fn draw_battle<B: Backend>(
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
    let player_paragraph = player_paragraph;

    let xp_bar = Gauge::default()
        .block(Block::default().title("XP").borders(Borders::ALL))
        .gauge_style(Style::default().fg(Color::Green))
        .label(format!("{}/{}", player.current_lvl_xp, player.next_lvl_xp))
        .percent(((player.current_lvl_xp as f32 / player.next_lvl_xp as f32) * 100.0) as u16);
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

    frame.render_widget(Clear, main_layout[0]);
    frame.render_widget(player_block, layout[0]);
    frame.render_widget(player_paragraph, player_block_layout[0]);
    frame.render_widget(xp_bar, player_block_layout[1]);
    frame.render_widget(enemy_health, enemy_layout[0]);
}
