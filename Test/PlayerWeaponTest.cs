using Core.Module.ItemData;
using Core.Module.Player;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Test;

public class PlayerWeaponTest : IClassFixture<PlayerInstanceFixture>
{
    private readonly PlayerInstance _playerInstance;
    private readonly ItemDataInit _itemDataInit;
    
    public PlayerWeaponTest(PlayerInstanceFixture playerInstanceFixture)
    {
        _playerInstance = playerInstanceFixture.GetPlayerInstance();
        _playerInstance.CharacterEffect().RemoveEffects();
        _itemDataInit = _playerInstance.ServiceProvider.GetRequiredService<ItemDataInit>();
    }

    /// <summary>
    /// Small Sword
    /// </summary>
    [Fact]
    public void AddSmallSwordTest()
    {
        var item = _itemDataInit.GetItemByName("small_sword");
        var itemInstance = new ItemInstance(0, _playerInstance.ServiceProvider)
        {
            ItemId = item.ItemId,
            ItemData = item,
            UserItemId = item.ItemId
        };
        _playerInstance.PlayerInventory().EquipItemInBodySlot(itemInstance);
        _playerInstance.PlayerInventory().AddItemToInventoryCollection(itemInstance);
        var pAtk = _playerInstance.CharacterCombat().GetPhysicalAttack();
        var pSpeed = _playerInstance.CharacterCombat().GetPhysicalAttackSpeed();
        var pCritical = _playerInstance.CharacterCombat().GetCriticalRate();
        Assert.Equal(8, pAtk);
        Assert.Equal(416, pSpeed);
        Assert.Equal(88, pCritical);
    }
    
    /// <summary>
    /// Sword
    /// </summary>
    [Fact]
    public void AddFalchionTest()
    {
        var item = _itemDataInit.GetItemByName("falchion");
        var itemInstance = new ItemInstance(1, _playerInstance.ServiceProvider)
        {
            ItemId = item.ItemId,
            ItemData = item,
            UserItemId = item.ItemId
        };
        _playerInstance.PlayerInventory().EquipItemInBodySlot(itemInstance);
        _playerInstance.PlayerInventory().AddItemToInventoryCollection(itemInstance);
        var pAtk = _playerInstance.CharacterCombat().GetPhysicalAttack();
        var pSpeed = _playerInstance.CharacterCombat().GetPhysicalAttackSpeed();
        var pCritical = _playerInstance.CharacterCombat().GetCriticalRate();
        var mAtk = _playerInstance.CharacterCombat().GetMagicalAttack();
        Assert.Equal(33, pAtk);
        Assert.Equal(416, pSpeed);
        Assert.Equal(88, pCritical);
        Assert.Equal(11, mAtk);
    }
    
    /// <summary>
    /// Two handed sword
    /// </summary>
    [Fact]
    public void AddZweihanderTest()
    {
        var item = _itemDataInit.GetItemByName("zweihander");
        var itemInstance = new ItemInstance(2, _playerInstance.ServiceProvider)
        {
            ItemId = item.ItemId,
            ItemData = item,
            UserItemId = item.ItemId
        };
        _playerInstance.PlayerInventory().EquipItemInBodySlot(itemInstance);
        _playerInstance.PlayerInventory().AddItemToInventoryCollection(itemInstance);
        var pAtk = _playerInstance.CharacterCombat().GetPhysicalAttack();
        var pSpeed = _playerInstance.CharacterCombat().GetPhysicalAttackSpeed();
        var pCritical = _playerInstance.CharacterCombat().GetCriticalRate();
        var mAtk = _playerInstance.CharacterCombat().GetMagicalAttack();
        Assert.Equal(41, pAtk);
        Assert.Equal(357, pSpeed);
        Assert.Equal(88, pCritical);
        Assert.Equal(11, mAtk);
    }
    
    /// <summary>
    /// Polearm test
    /// </summary>
    [Fact]
    public void AddLongSpearTest()
    {
        var item = _itemDataInit.GetItemByName("long_spear");
        var itemInstance = new ItemInstance(3, _playerInstance.ServiceProvider)
        {
            ItemId = item.ItemId,
            ItemData = item,
            UserItemId = item.ItemId
        };
        _playerInstance.PlayerInventory().EquipItemInBodySlot(itemInstance);
        _playerInstance.PlayerInventory().AddItemToInventoryCollection(itemInstance);
        var pAtk = _playerInstance.CharacterCombat().GetPhysicalAttack();
        var pSpeed = _playerInstance.CharacterCombat().GetPhysicalAttackSpeed();
        var pCritical = _playerInstance.CharacterCombat().GetCriticalRate();
        var mAtk = _playerInstance.CharacterCombat().GetMagicalAttack();
        Assert.Equal(33, pAtk);
        Assert.Equal(357, pSpeed);
        Assert.Equal(88, pCritical);
        Assert.Equal(11, mAtk);
    }
    
    /// <summary>
    /// Fist
    /// </summary>
    [Fact]
    public void AddViperFangTest()
    {
        var item = _itemDataInit.GetItemByName("shadow_weapon_viper_s_fang1");
        var itemInstance = new ItemInstance(3, _playerInstance.ServiceProvider)
        {
            ItemId = item.ItemId,
            ItemData = item,
            UserItemId = item.ItemId
        };
        _playerInstance.PlayerInventory().EquipItemInBodySlot(itemInstance);
        _playerInstance.PlayerInventory().AddItemToInventoryCollection(itemInstance);
        var pAtk = _playerInstance.CharacterCombat().GetPhysicalAttack();
        var pSpeed = _playerInstance.CharacterCombat().GetPhysicalAttackSpeed();
        var pCritical = _playerInstance.CharacterCombat().GetCriticalRate();
        var mAtk = _playerInstance.CharacterCombat().GetMagicalAttack();
        Assert.Equal(41, pAtk);
        Assert.Equal(357, pSpeed);
        Assert.Equal(44, pCritical);
        Assert.Equal(11, mAtk);
    }
    
    /// <summary>
    /// Dagger
    /// </summary>
    [Fact]
    public void AddSwordBreakerTest()
    {
        var item = _itemDataInit.GetItemByName("sword_breaker");
        var itemInstance = new ItemInstance(4, _playerInstance.ServiceProvider)
        {
            ItemId = item.ItemId,
            ItemData = item,
            UserItemId = item.ItemId
        };
        _playerInstance.PlayerInventory().EquipItemInBodySlot(itemInstance);
        _playerInstance.PlayerInventory().AddItemToInventoryCollection(itemInstance);
        var pAtk = _playerInstance.CharacterCombat().GetPhysicalAttack();
        var pSpeed = _playerInstance.CharacterCombat().GetPhysicalAttackSpeed();
        var pCritical = _playerInstance.CharacterCombat().GetCriticalRate();
        var mAtk = _playerInstance.CharacterCombat().GetMagicalAttack();
        Assert.Equal(29, pAtk);
        Assert.Equal(476, pSpeed);
        Assert.Equal(132, pCritical);
        Assert.Equal(11, mAtk);
    }
    
    /// <summary>
    /// Bow
    /// </summary>
    [Fact]
    public void AddCompositionBowTest()
    {
        var item = _itemDataInit.GetItemByName("composition_bow");
        var itemInstance = new ItemInstance(5, _playerInstance.ServiceProvider)
        {
            ItemId = item.ItemId,
            ItemData = item,
            UserItemId = item.ItemId
        };
        _playerInstance.PlayerInventory().EquipItemInBodySlot(itemInstance);
        _playerInstance.PlayerInventory().AddItemToInventoryCollection(itemInstance);
        var pAtk = _playerInstance.CharacterCombat().GetPhysicalAttack();
        var pSpeed = _playerInstance.CharacterCombat().GetPhysicalAttackSpeed();
        var pCritical = _playerInstance.CharacterCombat().GetCriticalRate();
        var mAtk = _playerInstance.CharacterCombat().GetMagicalAttack();
        Assert.Equal(69, pAtk);
        Assert.Equal(322, pSpeed);
        Assert.Equal(132, pCritical);
        Assert.Equal(11, mAtk);
    }
    
    /// <summary>
    /// Blunt
    /// </summary>
    [Fact]
    public void AddBuzdyganTest()
    {
        var item = _itemDataInit.GetItemByName("buzdygan");
        var itemInstance = new ItemInstance(6, _playerInstance.ServiceProvider)
        {
            ItemId = item.ItemId,
            ItemData = item,
            UserItemId = item.ItemId
        };
        _playerInstance.PlayerInventory().EquipItemInBodySlot(itemInstance);
        _playerInstance.PlayerInventory().AddItemToInventoryCollection(itemInstance);
        var pAtk = _playerInstance.CharacterCombat().GetPhysicalAttack();
        var pSpeed = _playerInstance.CharacterCombat().GetPhysicalAttackSpeed();
        var pCritical = _playerInstance.CharacterCombat().GetCriticalRate();
        var mAtk = _playerInstance.CharacterCombat().GetMagicalAttack();
        Assert.Equal(33, pAtk);
        Assert.Equal(416, pSpeed);
        Assert.Equal(44, pCritical);
        Assert.Equal(11, mAtk);
    }
    
    /// <summary>
    /// Mystic Weapon
    /// </summary>
    [Fact]
    public void AddMageStaffTest()
    {
        var item = _itemDataInit.GetItemByName("mage_staff");
        var itemInstance = new ItemInstance(7, _playerInstance.ServiceProvider)
        {
            ItemId = item.ItemId,
            ItemData = item,
            UserItemId = item.ItemId
        };
        _playerInstance.PlayerInventory().EquipItemInBodySlot(itemInstance);
        _playerInstance.PlayerInventory().AddItemToInventoryCollection(itemInstance);
        var pAtk = _playerInstance.CharacterCombat().GetPhysicalAttack();
        var pSpeed = _playerInstance.CharacterCombat().GetPhysicalAttackSpeed();
        var pCritical = _playerInstance.CharacterCombat().GetCriticalRate();
        var mAtk = _playerInstance.CharacterCombat().GetMagicalAttack();
        var castSpeed = _playerInstance.PlayerCombat().GetCastSpeed();
        
        Assert.Equal(32, pAtk);
        Assert.Equal(357, pSpeed);
        Assert.Equal(44, pCritical);
        Assert.Equal(14, mAtk);
        Assert.Equal(213, castSpeed);
    }
    
    /// <summary>
    /// Dual
    /// </summary>
    [Fact]
    public void AddChronoMaracasTest()
    {
        var item = _itemDataInit.GetItemByName("chrono_maracas");
        var itemInstance = new ItemInstance(8, _playerInstance.ServiceProvider)
        {
            ItemId = item.ItemId,
            ItemData = item,
            UserItemId = item.ItemId
        };
        _playerInstance.PlayerInventory().EquipItemInBodySlot(itemInstance);
        _playerInstance.PlayerInventory().AddItemToInventoryCollection(itemInstance);
        var pAtk = _playerInstance.CharacterCombat().GetPhysicalAttack();
        var pSpeed = _playerInstance.CharacterCombat().GetPhysicalAttackSpeed();
        var pCritical = _playerInstance.CharacterCombat().GetCriticalRate();
        var mAtk = _playerInstance.CharacterCombat().GetMagicalAttack();
        var castSpeed = _playerInstance.PlayerCombat().GetCastSpeed();
        
        Assert.Equal(1, pAtk);
        Assert.Equal(357, pSpeed);
        Assert.Equal(88, pCritical);
        Assert.Equal(0, mAtk);
        Assert.Equal(213, castSpeed);
    }
}