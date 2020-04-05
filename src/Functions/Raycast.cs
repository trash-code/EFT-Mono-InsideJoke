using EFT;
using UnityEngine;

namespace Exception5.Functions
{
    class Raycast
    {
        private static int vis_mask = 1 << 12 | 1 << 16;
        private static RaycastHit raycastHit;
        public static bool isVisible(Player player)
        {
            return Physics.Linecast(
                Camera.main.transform.position,
                player.PlayerBones.Head.position,
                out raycastHit,
                vis_mask) && raycastHit.collider && raycastHit.collider.gameObject.transform.root.gameObject == player.gameObject.transform.root.gameObject;
        }
        public static bool isBodyPartVisible(Player player, int bodypart) {
            Vector3 BodyPartToAim = Exception6.Functions.PlayerBones.FinalVector(player.PlayerBody.SkeletonRootJoint, bodypart);
            return Physics.Linecast(
                Camera.main.transform.position,
                BodyPartToAim,
                out raycastHit,
                vis_mask) && raycastHit.collider && raycastHit.collider.gameObject.transform.root.gameObject == player.gameObject.transform.root.gameObject;
        }
        public static Vector3 BarrelRaycast(Player LocalPlayer)
        {
            try
            {
                if (LocalPlayer.Fireport == null)
                    return Vector3.zero;

                Physics.Linecast(
                    LocalPlayer.Fireport.position, 
                    LocalPlayer.Fireport.position - LocalPlayer.Fireport.up * 1000f, 
                    out raycastHit, 
                    vis_mask);

                return raycastHit.point;
            }
            catch
            {
                return Vector3.zero;
            }
        }
    }
}
