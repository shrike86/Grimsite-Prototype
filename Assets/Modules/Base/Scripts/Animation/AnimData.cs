using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grimsite.Base
{
    public class AnimData 
    {
        public Transform leftFoot;
        public Transform rightFoot;

        public AnimData(Animator anim)
        {
            leftFoot = anim.GetBoneTransform(HumanBodyBones.LeftFoot);
            rightFoot = anim.GetBoneTransform(HumanBodyBones.RightFoot);
        }
    }
}