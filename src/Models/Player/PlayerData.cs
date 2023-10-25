using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokebot.Models.Player
{
    //gPlayerFacingPosition
    //gPlayerAvatar
    public class PlayerData
    {
        public int CurrentX { get; }
        public int CurrentY { get; }
        public int PreviousX { get; }
        public int PreviousY { get; }
        public PlayerRunningState RunningState { get; }
        public TileTransitionState TransitionState { get; }
        public bool Gender { get; }
        public PlayerFacingDirection FacingDirection { get; }

        public PlayerData(int currentX, int currentY, int previousX, int previousY, PlayerRunningState runningState, TileTransitionState transitionState, bool gender, PlayerFacingDirection facingDirection)
        {
            CurrentX = currentX;
            CurrentY = currentY;
            PreviousX = previousX;
            PreviousY = previousY;
            RunningState = runningState;
            TransitionState = transitionState;
            Gender = gender;
            FacingDirection = facingDirection;
        }
    }
}
