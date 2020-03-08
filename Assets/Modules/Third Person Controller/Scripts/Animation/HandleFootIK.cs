using Grimsite.Base;
using UnityEngine;

namespace Grimsite.ThirdPersonController
{
    [CreateAssetMenu(menuName = "Behaviour/State Actions/Animations/Handle Foot IK")]
    public class HandleFootIK : StateActions
    {
        public bool enableFeetIk = true;
        [Range(0, 2)] [SerializeField] private float heightFromGroundRaycast = 1.14f;
        [Range(0, 2)] [SerializeField] private float raycastDownDistance = 1.5f;
        [SerializeField] private LayerMask environmentLayer;
        [SerializeField] private float pelvisOffset = 0f;
        [Range(0, 1)] [SerializeField] private float pelvisUpAndDownSpeed = 0.28f;
        [Range(0, 1)] [SerializeField] private float feetToIkPositionSpeed = 0.5f;

        public string leftFootAnimVariableName = "leftFootIK";
        public string rightFootAnimVariableName = "rightFootIK";

        public bool useProIkFeature = false;
        public bool showSolverDebug = true;

        private Vector3 rightFootPosition, leftFootPosition, leftFootIkPosition, rightFootIkPosition;
        private Quaternion leftFootIkRotation, rightFootIkRotation;
        private float lastPelvisPositionY, lastRightFootPositionY, lastLeftFootPositionY;


        PlayerStateManager states;

        public void FixedExecute()
        {
            AdjustFeetTarget(ref rightFootPosition, HumanBodyBones.RightFoot);
            AdjustFeetTarget(ref leftFootPosition, HumanBodyBones.LeftFoot);

            //find and raycast to the ground to find positions
            FeetPositionSolver(rightFootPosition, ref rightFootIkPosition, ref rightFootIkRotation); // handle the solver for right foot
            FeetPositionSolver(leftFootPosition, ref leftFootIkPosition, ref leftFootIkRotation); //handle the solver for the left foot
        }

        public override void Execute(CharacterStateManager characterStates)
        {
            if (states == null)
                Init(characterStates);

            if (enableFeetIk == false) { return; }
            if (states.anim == null) { return; }

            FixedExecute();
            //MovePelvisHeight();

            //right foot ik position and rotation -- utilise the pro features in here
            states.anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);

            if (useProIkFeature)
            {
                states.anim.SetIKRotationWeight(AvatarIKGoal.RightFoot, states.anim.GetFloat(rightFootAnimVariableName));
            }

            MoveFeetToIkPoint(AvatarIKGoal.RightFoot, rightFootIkPosition, rightFootIkRotation, ref lastRightFootPositionY);

            //left foot ik position and rotation -- utilise the pro features in here
            states.anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);

            if (useProIkFeature)
            {
                states.anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, states.anim.GetFloat(leftFootAnimVariableName));
            }

            MoveFeetToIkPoint(AvatarIKGoal.LeftFoot, leftFootIkPosition, leftFootIkRotation, ref lastLeftFootPositionY);
        }

        public override void Init(CharacterStateManager characterStates)
        {
            states = characterStates as PlayerStateManager;
        }

        private void MovePelvisHeight()
        {

            if (rightFootIkPosition == Vector3.zero || leftFootIkPosition == Vector3.zero || lastPelvisPositionY == 0)
            {
                lastPelvisPositionY = states.anim.bodyPosition.y;
                return;
            }

            float lOffsetPosition = leftFootIkPosition.y - states.mTransform.position.y;
            float rOffsetPosition = rightFootIkPosition.y - states.mTransform.position.y;

            float totalOffset = (lOffsetPosition < rOffsetPosition) ? lOffsetPosition : rOffsetPosition;

            Vector3 newPelvisPosition = states.anim.bodyPosition + Vector3.up * totalOffset;

            newPelvisPosition.y = Mathf.Lerp(lastPelvisPositionY, newPelvisPosition.y, pelvisUpAndDownSpeed);

            states.anim.bodyPosition = newPelvisPosition;

            lastPelvisPositionY = states.anim.bodyPosition.y;

        }

        void MoveFeetToIkPoint(AvatarIKGoal foot, Vector3 positionIkHolder, Quaternion rotationIkHolder, ref float lastFootPositionY)
        {
            Vector3 targetIkPosition = states.anim.GetIKPosition(foot);

            if (positionIkHolder != Vector3.zero)
            {
                targetIkPosition = states.mTransform.InverseTransformPoint(targetIkPosition);
                positionIkHolder = states.mTransform.InverseTransformPoint(positionIkHolder);

                float yVariable = Mathf.Lerp(lastFootPositionY, positionIkHolder.y, feetToIkPositionSpeed);
                targetIkPosition.y += yVariable;

                lastFootPositionY = yVariable;

                targetIkPosition = states.mTransform.TransformPoint(targetIkPosition);

                states.anim.SetIKRotation(foot, rotationIkHolder);
            }

            states.anim.SetIKPosition(foot, targetIkPosition);
        }

        private void AdjustFeetTarget(ref Vector3 feetPositions, HumanBodyBones foot)
        {
            feetPositions = states.anim.GetBoneTransform(foot).position;
            feetPositions.y = states.mTransform.position.y + heightFromGroundRaycast;

        }

        private void FeetPositionSolver(Vector3 fromSkyPosition, ref Vector3 feetIkPositions, ref Quaternion feetIkRotations)
        {
            //raycast handling section 
            RaycastHit feetOutHit;

            if (showSolverDebug)
                Debug.DrawLine(fromSkyPosition, fromSkyPosition + Vector3.down * (raycastDownDistance + heightFromGroundRaycast), Color.yellow);

            if (Physics.Raycast(fromSkyPosition, Vector3.down, out feetOutHit, raycastDownDistance + heightFromGroundRaycast, environmentLayer))
            {
                //finding our feet ik positions from the sky position
                feetIkPositions = fromSkyPosition;
                feetIkPositions.y = feetOutHit.point.y + pelvisOffset;
                feetIkRotations = Quaternion.FromToRotation(Vector3.up, feetOutHit.normal) * states.mTransform.rotation;

                return;
            }

            feetIkPositions = Vector3.zero; //it didn't work :(

        }

        #region SA Implementation
        //Animator anim;
        //PlayerStateManager states;

        //Transform leftFoot;
        //Transform rightFoot;

        //Transform l_helper;
        //Transform r_helper;

        //float lf_weight;
        //float rf_weight;

        //LayerMask ignoreLayers;

        //Vector3 lf_pos;
        //Quaternion lf_rot;
        //Vector3 rf_pos;
        //Quaternion rf_rot;
        //Vector3 offset = new Vector3(0, 0.015f, 0);

        //public bool hasInit = false;

        //public bool ignoreRotation;



        //private void OnEnable()
        //{
        //    hasInit = false;
        //}

        //public override void Execute(CharacterStateManager characterStates)
        //{
        //    if (!hasInit)
        //        Init(characterStates);

        //    FindPosition(leftFoot, l_helper, ref lf_pos, ref lf_rot); ;
        //    FindPosition(rightFoot, r_helper, ref rf_pos, ref rf_rot);

        //    lf_weight = anim.GetFloat("leftFootIK");
        //    rf_weight = anim.GetFloat("rightFootIK");

        //    anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, lf_weight);
        //    anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, lf_weight);

        //    anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, rf_weight);
        //    anim.SetIKRotationWeight(AvatarIKGoal.RightFoot, rf_weight);

        //    anim.SetIKPosition(AvatarIKGoal.LeftFoot, lf_pos);
        //    anim.SetIKPosition(AvatarIKGoal.RightFoot, rf_pos);

        //    //anim.SetIKRotation(AvatarIKGoal.LeftFoot, lf_rot);
        //    //anim.SetIKRotation(AvatarIKGoal.RightFoot, rf_rot);
        //}


        //private void FindPosition(Transform transform, Transform transHelper, ref Vector3 targetPosition, ref Quaternion targetRotation)
        //{
        //    RaycastHit hit;
        //    Vector3 origin = transform.position;
        //    origin += Vector3.up * 0.01f;

        //    Debug.DrawRay(origin, -Vector3.up);
        //    if (Physics.Raycast(origin, -Vector3.up, out hit))
        //    {
        //        ignoreRotation = false;

        //        if (hit.transform.gameObject.layer == 11)
        //            ignoreRotation = true;

        //        targetPosition = hit.point + offset;
        //        Vector3 dir = transHelper.position - transform.position;
        //        dir.y = 0;

        //        Quaternion rot = Quaternion.LookRotation(dir);

        //        if (!ignoreRotation)
        //            targetRotation = Quaternion.FromToRotation(Vector3.up, hit.normal) * rot;
        //        else
        //            targetRotation = rot;
        //    }
        //}

        //public override void Init(CharacterStateManager characterStates)
        //{
        //    states = characterStates as PlayerStateManager;
        //    hasInit = true;

        //    anim = states.anim;

        //    ignoreLayers = ~(1 << 0);

        //    leftFoot = states.animData.leftFoot;
        //    rightFoot = states.animData.rightFoot;

        //    l_helper = new GameObject().transform;
        //    l_helper.localScale = Vector3.one * 0.05f;
        //    l_helper.transform.parent = leftFoot;
        //    l_helper.localPosition = new Vector3(0.00464f, 0.01082f, -0.00786f);


        //    r_helper = new GameObject().transform;
        //    r_helper.localScale = Vector3.one * 0.05f;
        //    r_helper.transform.parent = rightFoot;
        //    r_helper.localPosition = new Vector3(-0.00463f, 0.01012f, -0.00671f);
        //}

        #endregion
    }
}