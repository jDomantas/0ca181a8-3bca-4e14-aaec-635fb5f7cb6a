namespace _0ca181a8_3bca_4e14_aaec_635fb5f7cb6a
{
    interface ISceneHost
    {
        int ScreenWidth { get; }
        int ScreenHeight { get; }
        void SetScene(IScene scene);
    }
}
