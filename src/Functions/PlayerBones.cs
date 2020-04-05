using EFT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Exception6.Functions
{
    public class PlayerBones
    {
        public enum AimBody {
            Null = 0,
            Pelvis = 14,
            LThigh1 = 15, // killa
            LThigh2 = 16, // killa
            LCalf = 17, // killa
            LFoot = 18, // killa
            RThigh1 = 20, // killa
            RThigh2 = 21, // killa
            RCalf = 22, // killa
            RFoot = 23, // killa
            Ribcage = 66,
            LCollarbone = 89,
            LUpperarm = 90,
            LForearm1 = 91,
            LForearm2 = 92,
            LForearm3 = 93,
            LPalm = 94,
            RUpperarm = 111,
            RForearm1 = 112,
            RForearm2 = 113,
            RForearm3 = 114,
            RPalm = 115,
            Neck = 132,
            Head = 133
        }
        public enum BodyPart
        {
            Null = 0,
            Pelvis = 14,
            LThigh1 = 15,
            LThigh2 = 16,
            LCalf = 17,//
            LFoot = 18,
            LToe = 19,
            RThigh1 = 20,
            RThigh2 = 21,
            RCalf = 22,//
            RFoot = 23,
            RToe = 24,
            Bear_Feet = 25,
            USEC_Feet = 26,
            BEAR_feet_1 = 27,
            Spine1 = 29,
            Gear1 = 30,
            Gear2 = 31,
            Gear3 = 32,
            Gear4 = 33,
            Gear4_1 = 34,
            Gear5 = 35,
            Spine2 = 36,
            Spine3 = 37,
            Ribcage = 66,
            LCollarbone = 89,
            LUpperarm = 90,
            LForearm1 = 91,
            LForearm2 = 92,
            LForearm3 = 93,
            LPalm = 94,
            RUpperarm = 111,
            RForearm1 = 112,
            RForearm2 = 113,
            RForearm3 = 114,
            RPalm = 115,
            Neck = 132,
            Head = 133
        }

        public static Vector3 FinalVector(Diz.Skinning.Skeleton skeletor, int BoneId)
        {
            try
            {
                return skeletor.Bones.ElementAt(BoneId).Value.position;
            } catch { return Vector3.zero; }
        }
        public static string BoneName(Diz.Skinning.Skeleton sko, int id)
        {
            return sko.Bones.ElementAt(id).Key.ToString();
        }
        public static Vector3 GetBoneById(Player p, int BoneId)
        {
            return FinalVector(p.PlayerBody.SkeletonRootJoint, BoneId);
        }
    }
}
