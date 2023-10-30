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
        public Position Position { get; }
        public Position PreviousPosition { get; }
        public PlayerRunningState RunningState { get; }
        public TileTransitionState TransitionState { get; }
        public bool Gender { get; }
        public PlayerFacingDirection FacingDirection { get; }

        public PlayerData(Position position, Position prevPosition, PlayerRunningState runningState, TileTransitionState transitionState, bool gender, PlayerFacingDirection facingDirection)
        {
            Position = position;
            PreviousPosition = prevPosition;
            RunningState = runningState;
            TransitionState = transitionState;
            Gender = gender;
            FacingDirection = facingDirection;
        }
    }
}
