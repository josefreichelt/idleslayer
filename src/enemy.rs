pub struct Enemy {
    pub health: i64,
    pub max_health: i64,
    pub name: String,
}

impl Enemy {
    pub fn new() -> Self {
        Enemy {
            health: 50,
            max_health: 50,
            name: String::from("Skeleton"),
        }
    }
}
