using Dapper.FluentMap.Mapping;

namespace DataBase.Entities.Map
{
    public class CharacterMap : EntityMap<CharacterEntity>
    {
        public CharacterMap()
        {
            Map(i => i.AccountName).ToColumn("account_name");
            Map(i => i.CharacterId).ToColumn("char_id");
            Map(i => i.CharacterName).ToColumn("char_name");
            Map(i => i.MaxCp).ToColumn("max_cp");
            Map(i => i.MaxHp).ToColumn("max_hp");
            Map(i => i.Cp).ToColumn("cp");
            Map(i => i.Hp).ToColumn("hp");
            Map(i => i.MaxMp).ToColumn("max_mp");
            Map(i => i.Mp).ToColumn("mp");
            Map(i => i.Exp).ToColumn("exp");
            Map(i => i.Gender).ToColumn("gender");
            Map(i => i.Race).ToColumn("race");
            Map(i => i.ClassId).ToColumn("class");
            Map(i => i.XLoc).ToColumn("xloc");
            Map(i => i.YLoc).ToColumn("yloc");
            Map(i => i.ZLoc).ToColumn("zloc");
            Map(i => i.IsInVehicle).ToColumn("isInVehicle");
            Map(i => i.Level).ToColumn("lev");
            Map(i => i.Pk).ToColumn("pk");
            Map(i => i.Duel).ToColumn("duel");
            Map(i => i.StUnderwear).ToColumn("st_underware");
            Map(i => i.StRightEar).ToColumn("st_right_ear");
            Map(i => i.StLeftEar).ToColumn("st_left_ear");
            Map(i => i.StNeck).ToColumn("st_neck");
            Map(i => i.StRightFinger).ToColumn("st_right_finger");
            Map(i => i.StLeftFinger).ToColumn("st_left_finger");
            Map(i => i.StHead).ToColumn("st_head");
            Map(i => i.StRightHand).ToColumn("st_right_hand");
            Map(i => i.StLeftHand).ToColumn("st_left_hand");
            Map(i => i.StGloves).ToColumn("st_gloves");
            Map(i => i.StChest).ToColumn("st_chest");
            Map(i => i.StLegs).ToColumn("st_legs");
            Map(i => i.StFeet).ToColumn("st_feet");
            Map(i => i.StBack).ToColumn("st_back");
            Map(i => i.StBothHand).ToColumn("st_both_hand");
            Map(i => i.StHair).ToColumn("st_hair");
            Map(i => i.StFace).ToColumn("st_face");
            Map(i => i.StHairAll).ToColumn("st_hairall");
            Map(i => i.QuestFlag).ToColumn("quest_flag");
            Map(i => i.Nickname).ToColumn("nickname");
            Map(i => i.QuestMemo).ToColumn("quest_memo");
            Map(i => i.FaceIndex).ToColumn("face_index");
            Map(i => i.HairShapeIndex).ToColumn("hair_shape_index");
            Map(i => i.HairColorIndex).ToColumn("hair_color_index");
        }
    }
}
