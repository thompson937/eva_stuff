using NonogramModel.GameState;

namespace NonogramModel
{
    public class NonogramModel : INonogramModel
    {
        public IGameState? gameState { get; }


        public NonogramModel() { }

        public void Load()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void New()
        {
            throw new NotImplementedException();
        }
    }
}
