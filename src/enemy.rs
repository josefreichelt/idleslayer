pub struct Enemy {
    pub health: i64,
    pub max_health: i64,
    pub name: String,
    pub xp: u64,
    pub gold: u64,
}

impl Enemy {
    pub fn new() -> Self {
        Enemy {
            health: 50,
            max_health: 50,
            xp: 10,
            name: String::from("Skeleton"),
            gold: 1,
        }
    }

    pub fn health_percentage(&self) -> u16 {
        (self.health as f32 / self.max_health as f32 * 100.0) as u16
    }
}
