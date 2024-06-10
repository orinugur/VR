using UnityEngine;
using Unity.XR.CoreUtils;
using UnityEngine.XR.Interaction.Toolkit;

public class LocomotionManager : MonoBehaviour
{
    public enum MoveStyleType { HeadRelative, HandRelative }
    public enum TurnStyleType { Snap, Continuous }

    [Header("XR")]
    [SerializeField] XROrigin XROrigin;
    [SerializeField] XRBaseController Controller_Left;
    [SerializeField] XRBaseController Controller_Right;

    [Header("Locomotion")]
    [SerializeField] ContinuousMoveProviderBase Provider_ContinuousMove;
    [SerializeField] ContinuousTurnProviderBase Provider_ContinuousTurn;
    [SerializeField] SnapTurnProviderBase Provider_SnapTurn;
    [SerializeField] TeleportationProvider Provider_Teleportation;

    [Header("Property")]
    [SerializeField] MoveStyleType _leftHandMoveStyle;
    [SerializeField] TurnStyleType _rightHandTurnStyle;


    public MoveStyleType MoveStyle
    {
        get { return _leftHandMoveStyle; }
        set
        {
            _leftHandMoveStyle = value;
            switch (_leftHandMoveStyle)
            {
                case MoveStyleType.HeadRelative:
                    Provider_ContinuousMove.forwardSource = XROrigin.Camera.transform;
                    break;
                case MoveStyleType.HandRelative:
                    Provider_ContinuousMove.forwardSource = Controller_Left.transform;
                    break;
            }
        }
    }

    public TurnStyleType TurnStyle
    {
        get { return _rightHandTurnStyle; }
        set
        {
            _rightHandTurnStyle = value;
            switch (_rightHandTurnStyle)
            {
                case TurnStyleType.Snap:
                    Provider_ContinuousTurn.enabled = false;
                    Provider_SnapTurn.enabled = true;
                    break;
                case TurnStyleType.Continuous:
                    Provider_ContinuousTurn.enabled = true;
                    Provider_SnapTurn.enabled = false;
                    break;
            }
        }
    }

}
