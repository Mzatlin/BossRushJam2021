internal interface IPlayerStats
{
    bool GetPlayerDeath();
    bool GetPlayerReadiness();
    void SetPlayerReadiness(bool isready);
    void SetPlayerDeath(bool isdead);
    void SetPlayerPaused(bool isPaused);

}