use ratatui::{text::Spans, widgets::Paragraph};

pub struct Player {
    pub lvl: u16,
    pub next_lvl_xp: u64,
    pub current_lvl_xp: u64,
    pub total_xp: u64,
    pub damage: u64,
    pub name: String,
    pub gold: u64,
}

impl Player {
    pub fn new() -> Self {
        Player {
            lvl: 1,
            current_lvl_xp: 0,
            next_lvl_xp: 100,
            total_xp: 0,
            damage: 1,
            name: String::from("Bob"),
            gold: 0,
        }
    }

    pub fn into_paragraph(&self) -> Paragraph {
        let text = vec![
            Spans::from(format!("Name: {}", self.name.as_str())),
            Spans::from(format!("XP: Total: {} | Current: {} | Next: LVL {}", self.total_xp,self.current_lvl_xp,self.next_lvl_xp)),
            Spans::from(format!("LVL: {}", self.lvl)),
            Spans::from(format!("Damage: {}", self.damage)),
            Spans::from(format!("Gold 🪙: {}", self.gold)),
        ];

        Paragraph::new(text)
    }
}
