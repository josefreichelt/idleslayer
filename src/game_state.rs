use crate::{player::Player, enemy::Enemy};

pub struct GameState {
    pub player: Player,
    pub enemy: Enemy,
    pub should_quit: bool,
    pub menu_state: MenuState,
    pub is_paused: bool,
}

pub enum MenuState {
    MainMenu,
    Battle,
    Skills
}

impl Default for MenuState {
    fn default() -> Self {
        Self::Battle
    }
}
