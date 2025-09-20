classDiagram

class ScreenShake {
    +IEnumerator Shake(float duration, float magnitude)
}

class PlayerHealth {
    -int startingHealth
    -Text healthText
    -int currentHealth
    -ScreenShake screenShake
    +int StartingHealth
    +int CurrentHealth
    +void TakeDamage(int amount)
    +void UpdateHealthUI()
    +void LoadGameOverScene()
}

class NextScene {
    +void LoadScene(int sceneIndex)
    +void Quit()
}

class MoneySystem {
    -int money
    -Text moneyText
    -Button spawnButton
    +int Money
    +void AddMoney(int amount)
    +void SpendMoney(int amount)
    +void UpdateMoneyUI()
}

class TowerSpawner {
    -GameObject prefab
    -Button spawnButton
    -MoneySystem moneySystem
    -Camera mainCamera
    -GameObject spawnedObject
    -bool isPlacing
    -const float checkRadius
    +void SpawnObject()
    +bool IsCollidingWithTower(Vector3 position)
    +void EnableCollider(GameObject obj, bool enable)
}

class Tower {
    -float range
    -GameObject Bullet
    -Transform shootPoint
    -float baseFireRate
    -Transform target
    -float fireCountdown
    -static List~Tower~ allTowers
    -static float fireRateUpgrade
    -float fireRate
    +void FindTarget()
    +void Shoot()
    +static void UpgradeFireRate(float amount)
}

class GameManager {
    +static GameManager instance
    +float fireRateUpgrade
    +float bulletSpeedUpgrade
    -MoneySystem moneySystem
    +void BuyFireRateUpgrade()
}

class Bullets {
    -float speed
    -float explosionRadius
    -GameObject impactEffect
    -Transform target
    -MoneySystem moneySystem
    +void Seek(Transform _target)
    +void HitTarget()
    +void Explode()
    +void Damage(Transform enemy)
    +void IncreaseSpeed(float amount)
}

class EnemySpawner {
    -GameObject enemyPrefab
    -Transform[] waypoints
    -float spawnWaitTime
    -float timeBetweenWaves
    -int minEnemiesPerWave
    -int maxEnemiesPerWave
    -float speedIncrement
    -Text waveText
    -int enemiesInThisWave
    -bool waveInProgress
    -float currentEnemySpeed
    -int currentWave
    +IEnumerator SpawnWaves()
    +bool AllEnemiesDestroyed()
    +void UpdateEnemySpeeds(float newSpeed)
}

class EnemyMovement {
    -Transform[] waypoints
    -float speed
    -PlayerHealth playerHealth
    -int waypointIndex
    +void MoveTowardsWaypoint()
}

PlayerHealth ..> ScreenShake
PlayerHealth ..> Text
PlayerHealth ..> SceneManager

MoneySystem ..> Text
MoneySystem ..> Button

TowerSpawner ..> GameObject
TowerSpawner ..> Button
TowerSpawner ..> MoneySystem
TowerSpawner ..> Camera

Tower ..> GameObject
Tower ..> Transform
Tower ..> Bullets

GameManager ..> MoneySystem
GameManager ..> Tower
GameManager ..> Bullets

Bullets ..> Transform
Bullets ..> MoneySystem

EnemySpawner ..> GameObject
EnemySpawner ..> Transform
EnemySpawner ..> Text
EnemySpawner ..> EnemyMovement

EnemyMovement ..> PlayerHealth