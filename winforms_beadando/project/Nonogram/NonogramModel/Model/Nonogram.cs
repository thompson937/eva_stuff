using NonogramModel.GameStates;

namespace NonogramModel
{
    public class Nonogram : INonogram
    {
        public IGameState? GameState { get; set; }


        public Nonogram() { }

        public void LoadGame()
        {
            throw new NotImplementedException();
        }

        public void SaveGame()
        {
            throw new NotImplementedException();
        }

        public void NewGame()
        {
            bool[] testImage =
            {
                false, false, true, false, false,
                false, true, false, true, false,
                true, false, false, false, false,
                true, true, false, true, true,
                false, false, true, false, false
            };

            GameState = new GameState(5, testImage);
        }
    }
}
