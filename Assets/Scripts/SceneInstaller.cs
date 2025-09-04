using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller<SceneInstaller>
{
    [SerializeField]
    private ChosenOneController chosenOneController;

    [SerializeField]
    private CursorManager cursorManager;

    [SerializeField]
    private Inventory inventory;

    public override void InstallBindings()
    {
        Container.Bind<ChosenOneController>()
            .FromInstance(chosenOneController)
            .AsSingle()
            .NonLazy();
        Container.Bind<CursorManager>()
             .FromInstance(cursorManager)
             .AsSingle()
             .NonLazy();
        Container.Bind<Inventory>()
            .FromInstance(inventory)
            .AsSingle()
            .NonLazy();
    }
}
