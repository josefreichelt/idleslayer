use crate::{player::Player, enemy::Enemy};

pub struct GameState {
    pub player: Player,
    pub enemy: Enemy,
    pub should_quit: bool,
    pub menu_state: MenuState,
}

pub enum MenuState {
    MainMenu,
    Battle,
    Skills
}
