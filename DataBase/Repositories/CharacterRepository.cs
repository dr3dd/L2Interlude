using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DataBase.Entities;
using DataBase.Interfaces;
using L2Logger;
using Microsoft.Extensions.DependencyInjection;

namespace DataBase.Repositories
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly GameConnectionFactory _connectionFactory;
        public CharacterRepository(IServiceProvider serviceProvider)
        {
            _connectionFactory = serviceProvider.GetService<GameConnectionFactory>();
        }
        
        public Task<CharacterEntity> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IReadOnlyList<CharacterEntity>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<int> AddAsync(CharacterEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> UpdateAsync(CharacterEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<int> CreateCharacterAsync(CharacterEntity characterEntity)
        {
            try
            {
                using (var connection = _connectionFactory.GetDbConnection())
                {
                    connection.Open();
                    string sql =
                        "INSERT INTO user_data (char_name,account_name,account_id,gender,race,class,xloc,yloc,zloc,isInVehicle,cp,hp,mp,sp,exp,lev,pk,duel,st_underware,st_right_ear,st_left_ear,st_neck,st_right_finger,st_left_finger,st_head,st_right_hand,st_left_hand,st_gloves,st_chest,st_legs,st_feet,st_back,st_both_hand,st_hair,st_face,st_hairall,quest_flag,nickname,max_hp,max_mp,quest_memo,face_index,hair_shape_index,hair_color_index) values (@CharacterName,@AccountName,@AccountId,@Gender,@Race,@ClassId,@XLoc,@YLoc,@ZLoc,@IsInVehicle,@Cp,@Hp,@Mp,@Sp,@Exp,@Level,@Pk,@Duel,@StUnderwear,@StRightEar,@StLeftEar,@StNeck,@StRightFinger,@StLeftFinger,@StHead,@StRightHand,@StLeftHand,@StGloves,@StChest,@StLegs,@StFeet,@StBack,@StBothHand,@StHair,@StFace,@StHairAll,@QuestFlag,@Nickname,@MaxHp,@MaxMp,@QuestMemo,@FaceIndex,@HairShapeIndex,@HairColorIndex); SELECT LAST_INSERT_ID();";

                    var characterId = await connection.ExecuteScalarAsync<int>(sql, characterEntity);
                    return characterId;
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Error(ex.Message);
                throw;                
            }
        }

        public async Task<CharacterEntity> UpdateCharacterAsync(CharacterEntity characterEntity)
        {
            try
            {
                using (var connection = _connectionFactory.GetDbConnection())
                {
                    connection.Open();
                    string sql =
                        "UPDATE user_data SET lev=@Level,max_hp=@MaxHp,hp=@Hp,max_mp=@MaxMp,mp=@Mp,exp=@Exp,sp=@Sp,xloc=@XLoc,yloc=@YLoc,zloc=@ZLoc, st_underware=@StUnderwear, st_right_ear=@StRightEar, st_left_ear=@StLeftEar, st_neck=@StNeck, st_right_finger=@StRightFinger, st_left_finger=@StLeftFinger, st_head=@StHead, st_right_hand=@StRightHand, st_left_hand=@StLeftHand, st_gloves=@StGloves, st_chest=@StChest, st_legs=@StLegs, st_feet=@StFeet, st_back=@StBack, st_both_hand=@StBothHand, st_hair=@StHair, st_face=@StFace, st_hairall=@StHairAll, quest_flag=@QuestFlag WHERE char_id=@CharacterId";
                    await connection.ExecuteAsync(sql, characterEntity);
                    return characterEntity;
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Error(ex.Message);
                throw;                
            }
        }

        public async Task<List<CharacterEntity>> GetCharactersByAccountNameAsync(string accountName)
        {
            try
            {
                using (var connection = _connectionFactory.GetDbConnection())
                {
                    connection.Open();
                    string sql = "SELECT * FROM user_data WHERE account_name = @AccountName";
                
                    IEnumerable<CharacterEntity> characters = await connection.QueryAsync<CharacterEntity>(sql, new {AccountName = accountName});
                    return characters.ToList();
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Error(ex.Message);
                throw;
            }
        }

        public async Task<CharacterEntity> GetCharacterByObjectIdAsync(int charId)
        {
            try
            {
                using (var connection = _connectionFactory.GetDbConnection())
                {
                    connection.Open();
                    string sql = "SELECT * FROM user_data WHERE char_id = @CharacterId";
                    IEnumerable<CharacterEntity> characters = await connection.QueryAsync<CharacterEntity>(sql, new {CharacterId = charId});
                    return characters.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Error(ex.Message);
                throw;
            }
        }

        public bool IsCharacterExist(string characterName)
        {
            try
            {
                using (var connection = _connectionFactory.GetDbConnection())
                {
                    connection.Open();
                    string sql = "SELECT EXISTS(SELECT char_name FROM user_data WHERE char_name = @CharacterName)"; 
                    return connection.ExecuteScalar<bool>(sql, new {CharacterName = characterName});
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Error(ex.Message);
                throw;
            }
        }

        public int GetMaxObjectId()
        {
            try
            {
                using (var connection = _connectionFactory.GetDbConnection())
                {
                    connection.Open();
                    int maxItemId = connection.ExecuteScalar<int>("SELECT max(item_id) FROM user_item;");
                    int maxCharacterId = connection.ExecuteScalar<int>("SELECT max(char_Id) FROM user_data;");
                    return Math.Max(maxCharacterId, maxItemId);
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Error(ex.Message);
                throw;
            }
        }
    }
}