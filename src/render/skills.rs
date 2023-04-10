use std::rc::Rc;

use ratatui::{
    backend::Backend,
    layout::{Layout, Rect, Alignment, Constraint},
    widgets::{Block, Borders, Clear},
    Frame,
};

use crate::{enemy::Enemy, player::Player};

pub fn draw_skills<B: Backend>(
    frame: &mut Frame<B>,
    main_layout: Rect,
    player: &mut Player,
    enemy: &mut Enemy,
) {
    let layout = Layout::default()
    .constraints([
        Constraint::Percentage(100),
        ].as_ref())
        .split(main_layout);
    let skills_block = Block::default()
    .title("[ Skills Shop] ")
    .borders(Borders::ALL)
    .title_alignment(Alignment::Center);


    frame.render_widget(Clear, main_layout);
    frame.render_widget(Clear, layout[0]);
    frame.render_widget(skills_block, layout[0]);
}
