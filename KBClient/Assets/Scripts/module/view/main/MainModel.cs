using UnityEngine;
using System.Collections;
using xk_System.Model;
using xk_System.Debug;
using KBEngine;
using System;

namespace xk_System.Model.Modules
{
    public class MainModel : DataModel
    {
        public override void initModel()
        {
            base.initModel();
            installEvents();
        }

        public override void destroyModel()
        {
            base.destroyModel();
            KBEngine.Event.deregisterOut(this);
        }

        void installEvents()
        {
            // in world
            KBEngine.Event.registerOut("addSpaceGeometryMapping", this, "addSpaceGeometryMapping");
            KBEngine.Event.registerOut("onEnterWorld", this, "onEnterWorld");
            KBEngine.Event.registerOut("onLeaveWorld", this, "onLeaveWorld");
            KBEngine.Event.registerOut("set_position", this, "set_position");
            KBEngine.Event.registerOut("set_direction", this, "set_direction");
            KBEngine.Event.registerOut("updatePosition", this, "updatePosition");
            KBEngine.Event.registerOut("onControlled", this, "onControlled");

            // in world(register by scripts)
            KBEngine.Event.registerOut("onAvatarEnterWorld", this, "onAvatarEnterWorld");
            KBEngine.Event.registerOut("set_HP", this, "set_HP");
            KBEngine.Event.registerOut("set_MP", this, "set_MP");
            KBEngine.Event.registerOut("set_HP_Max", this, "set_HP_Max");
            KBEngine.Event.registerOut("set_MP_Max", this, "set_MP_Max");
            KBEngine.Event.registerOut("set_level", this, "set_level");
            KBEngine.Event.registerOut("set_name", this, "set_entityName");
            KBEngine.Event.registerOut("set_state", this, "set_state");
            KBEngine.Event.registerOut("set_moveSpeed", this, "set_moveSpeed");
            KBEngine.Event.registerOut("set_modelScale", this, "set_modelScale");
            KBEngine.Event.registerOut("set_modelID", this, "set_modelID");
            KBEngine.Event.registerOut("recvDamage", this, "recvDamage");
            KBEngine.Event.registerOut("otherAvatarOnJump", this, "otherAvatarOnJump");
            KBEngine.Event.registerOut("onAddSkill", this, "onAddSkill");
        }


        public void addSpaceGeometryMapping(string respath)
        {
            DebugSystem.Log("loading scene(" + respath + ")...");
           /* UI.inst.info("scene(" + respath + "), spaceID=" + KBEngineApp.app.spaceID);
            if (terrain == null)
                terrain = Instantiate(terrainPerfab) as UnityEngine.GameObject;

            if (player)
                player.GetComponent<GameEntity>().entityEnable();*/
        }

        public void onAvatarEnterWorld(UInt64 rndUUID, Int32 eid, KBEngine.Avatar avatar)
        {
            if (!avatar.isPlayer())
            {
                return;
            }

            UI.inst.info("loading scene...(加载场景中...)");
            DebugSystem.Log("loading scene...");
        }


        public void onEnterWorld(KBEngine.Entity entity)
        {
            if (entity.isPlayer())
                return;

            float y = entity.position.y;
            if (entity.isOnGround)
                y = 1.3f;

           /* entity.renderObj = Instantiate(entityPerfab, new Vector3(entity.position.x, y, entity.position.z),
                Quaternion.Euler(new Vector3(entity.direction.y, entity.direction.z, entity.direction.x))) as UnityEngine.GameObject;*/

            ((UnityEngine.GameObject)entity.renderObj).name = entity.className + "_" + entity.id;
        }

        public void onLeaveWorld(KBEngine.Entity entity)
        {
            if (entity.renderObj == null)
                return;

            UnityEngine.GameObject.Destroy((UnityEngine.GameObject)entity.renderObj);
            entity.renderObj = null;
        }

        public void set_position(KBEngine.Entity entity)
        {
            if (entity.renderObj == null)
                return;

            GameEntity gameEntity = ((UnityEngine.GameObject)entity.renderObj).GetComponent<GameEntity>();
            gameEntity.destPosition = entity.position;
            gameEntity.position = entity.position;
            gameEntity.spaceID = KBEngineApp.app.spaceID;
        }

        public void updatePosition(KBEngine.Entity entity)
        {
            if (entity.renderObj == null)
                return;

            GameEntity gameEntity = ((UnityEngine.GameObject)entity.renderObj).GetComponent<GameEntity>();
            gameEntity.destPosition = entity.position;
            gameEntity.isOnGround = entity.isOnGround;
            gameEntity.spaceID = KBEngineApp.app.spaceID;
        }

        public void onControlled(KBEngine.Entity entity, bool isControlled)
        {
            if (entity.renderObj == null)
                return;

            GameEntity gameEntity = ((UnityEngine.GameObject)entity.renderObj).GetComponent<GameEntity>();
            gameEntity.isControlled = isControlled;
        }

        public void set_direction(KBEngine.Entity entity)
        {
            if (entity.renderObj == null)
                return;

            GameEntity gameEntity = ((UnityEngine.GameObject)entity.renderObj).GetComponent<GameEntity>();
            gameEntity.destDirection = new Vector3(entity.direction.y, entity.direction.z, entity.direction.x);
            gameEntity.spaceID = KBEngineApp.app.spaceID;
        }

        public void set_HP(KBEngine.Entity entity, object v)
        {
            if (entity.renderObj != null)
            {
                ((UnityEngine.GameObject)entity.renderObj).GetComponent<GameEntity>().hp = "" + (Int32)v + "/" + (Int32)entity.getDefinedProperty("HP_Max");
            }
        }

        public void set_MP(KBEngine.Entity entity, object v)
        {
        }

        public void set_HP_Max(KBEngine.Entity entity, object v)
        {
            if (entity.renderObj != null)
            {
                ((UnityEngine.GameObject)entity.renderObj).GetComponent<GameEntity>().hp = (Int32)entity.getDefinedProperty("HP") + "/" + (Int32)v;
            }
        }

        public void set_MP_Max(KBEngine.Entity entity, object v)
        {
        }

        public void set_level(KBEngine.Entity entity, object v)
        {
        }

        public void set_entityName(KBEngine.Entity entity, object v)
        {
            if (entity.renderObj != null)
            {
                ((UnityEngine.GameObject)entity.renderObj).GetComponent<GameEntity>().entity_name = (string)v;
            }
        }

        public void set_state(KBEngine.Entity entity, object v)
        {
            if (entity.renderObj != null)
            {
                ((UnityEngine.GameObject)entity.renderObj).GetComponent<GameEntity>().set_state((SByte)v);
            }

            if (entity.isPlayer())
            {
                DebugSystem.Log("player->set_state: " + v);

                if (((SByte)v) == 1)
                    UI.inst.showReliveGUI = true;
                else
                    UI.inst.showReliveGUI = false;

                return;
            }
        }

        public void set_moveSpeed(KBEngine.Entity entity, object v)
        {
            float fspeed = ((float)(Byte)v) / 10f;

            if (entity.renderObj != null)
            {
                ((UnityEngine.GameObject)entity.renderObj).GetComponent<GameEntity>().speed = fspeed;
            }
        }

        public void set_modelScale(KBEngine.Entity entity, object v)
        {
        }

        public void set_modelID(KBEngine.Entity entity, object v)
        {
        }

        public void recvDamage(KBEngine.Entity entity, KBEngine.Entity attacker, Int32 skillID, Int32 damageType, Int32 damage)
        {
        }

        public void otherAvatarOnJump(KBEngine.Entity entity)
        {
            DebugSystem.Log("otherAvatarOnJump: " + entity.id);
            if (entity.renderObj != null)
            {
                ((UnityEngine.GameObject)entity.renderObj).GetComponent<GameEntity>().OnJump();
            }
        }




    }
}