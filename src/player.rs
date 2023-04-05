use ratatui::{text::Spans, widgets::Paragraph};

pub struct Player {
    pub lvl: u16,
    pub xp: u64,
    pub damage: u64,
    pub name: String,
}

impl Player {
    pub fn new() -> Self {
        Player {
            lvl: 1,
            xp: 0,
            damage: 1,
            name: String::from("Bob"),
        }
    }

    pub fn into_paragraph(&self) -> Paragraph {
        let text = vec![
            Spans::from(format!("Name: {}", self.name.as_str())),
            Spans::from(format!("XP: {}", self.xp)),
            Spans::from(format!("LVL: {}", self.lvl)),
            Spans::from(format!("Damage: {}", self.damage)),
        ];

        Paragraph::new(text)
    }
}
