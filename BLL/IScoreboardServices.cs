using Sra.P2rmis.Bll.Views.Scoreboard;

namespace Sra.P2rmis.Bll
{
    public interface IScoreboardServices
    {
        void Dispose();
        ScoreboardContainer GetScoreboardByApplicationIdPanelId(int panelApplicationId, int panelId);
  }
}
