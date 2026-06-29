public class Goblin : Enemy
{
    // El Goblin es un enemigo melee básico.
    // Toda la lógica ya está en Enemy.cs.
    protected override void Start()
    {
        base.Start();
        // Configuración específica del Goblin
        attackRange = 2.5f;
    }
}




