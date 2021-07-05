internal interface IPlayerStats
{
    bool GetPlayerReadiness();
    void SetPlayerReadiness(bool isready);
    void SetPlayerDeath(bool isdead);
}