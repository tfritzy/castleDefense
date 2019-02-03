
health = [42,49,72,153,234,141,169,379,217,320,264,513,299,464,409,401,509,524,593,624,619,592,738,711,807,822,1057,907,1025,967,1074,1071,1103,1120,1120,984,1176,1376,1760,2176,2248,2104,2144,2632,2644,2688,3032,3080,3128,3736,3852,3620,3964,4552,3928,4608,4072,6000,6500,5588,6136,5448]
gold = [3,7,5,12,15,9,11,19,14,28,33,24,35,26,22,27,23,23,22,54,39,52,38,40,45,68,79,53,52,87,61,66,77,62,6,4,8,11,13,13,15,23,18,19,47,57,41,37,46,39,99,54,91,50,81,82,76,93,75,173,87,112]


class Archer:
    
    
    def __init__(self):
        self.attackDamage = 4
        self.attackSpeed = 1
        self.count = 1
        self.critChance = 0
        self.critDamage = 2
        self.currentGold = 0

    def getDamage(self):
        return (((self.attackDamage + (self.attackDamage*self.critChance)*self.critDamage))*self.count)/(8/self.attackSpeed)

    def buyAttackDamage(self):
        if (self.currentGold > 6 * ((self.attackDamage - 2) / 2)):
            self.currentGold -= 6 * ((self.attackDamage - 2) / 2)
            self.attackDamage += 2
            
            #print("bought attack Damage")
            return True
        else:
            return False

    def buyAttackSpeed(self):
        if (self.currentGold > ((self.attackSpeed - 1)/.05)*8):
            self.currentGold -= ((self.attackSpeed - 1)/.05)*8
            self.attackSpeed += .05
            
            #print("bought attack speed")
            return True
        else:
            return False

    def buyArcher(self):
        if (self.currentGold > (self.count+1) * 50):
            
            self.currentGold -= (self.count+1) * 50
            self.count += 1
            #print("bought archer")
            return True
        else:
            return False

    def buyCritChance(self):
        if (self.currentGold > ((self.critChance * self.critChance) / .005)):
            
            self.currentGold -= ((self.critChance * self.critChance) / .005)
            self.critChance += .01
            #print("bought crit Damage")
            return True
        else:
            return False

    def buyCritDamage(self):
        if (self.currentGold > ((self.critDamage - 2) / .1) * 5):
            
            self.currentGold -= ((self.critDamage - 2) / .1) * 5
            self.critDamage += .1
            #print("bought crit Damage")
            return True
        else:
            return False

    def __str__(self):
        return "Attack Damage: " + str(self.attackDamage) + " Attack Speed: " + str(self.attackSpeed) + " Count: " + str(self.count) + " Crit Chance: " + str(self.critChance) + " Crit Damage: " + str(self.critDamage) + " Gold: " + str(self.currentGold) + " Damage: " + str(self.getDamage())

    def copy(self):
        copy = Archer()
        copy.attackDamage = self.attackDamage
        copy.attackSpeed = self.attackSpeed
        copy.count = self.count
        copy.critChance = self.critChance
        copy.critDamage = self.critDamage
        copy.currentGold = self.currentGold
        return copy

    

    def buyBest(self):
        startingGold = self.currentGold
        startingDamage = self.attackDamage
        startingCount = self.count
        startingCritChance = self.critChance
        startingCritDamage = self.critDamage
        startingAttackSpeed = self.attackSpeed
        
        startingArcher = self.copy()
        bestArcher = self.copy()
        testArcher = self.copy()
        
        best = "none"
        testArcher.buyAttackDamage();
        if (testArcher.getDamage() > bestArcher.getDamage()):
            bestArcher = testArcher
            best = "damage"

        testArcher = startingArcher.copy()
        testArcher.buyAttackSpeed()
        if (testArcher.getDamage() > bestArcher.getDamage()):
            bestArcher = testArcher
            best = "speed"
            
        testArcher = startingArcher.copy()
        testArcher.buyArcher()
        if (testArcher.getDamage() > bestArcher.getDamage()):
            bestArcher = testArcher
            best = "new archer"
            
        testArcher = startingArcher.copy()
        testArcher.buyCritChance()
        if (testArcher.getDamage() > bestArcher.getDamage()):
            bestArcher = testArcher
            best = "crit chance"
            
        testArcher = startingArcher.copy()
        testArcher.buyCritDamage()
        if (testArcher.getDamage() > bestArcher.getDamage()):
            bestArcher = testArcher
            best = "crit Damage"

        self.currentGold = bestArcher.currentGold
        self.attackDamage = bestArcher.attackDamage
        self.count = bestArcher.count
        self.critChance = bestArcher.critChance
        self.critDamage = bestArcher.critDamage
        self.attackSpeed = bestArcher.attackSpeed
        #print(str(self))
        print ("Best upgrade: " + best + " Cost: " + str(startingArcher.currentGold - bestArcher.currentGold))
        
        

archer = Archer()
archer.attackDamage = 4
archer.currentGold = 0
archer.count = 0
i = 0
deathCounter = 0
while(1):


    
    archer.currentGold += gold[i]
    print ("Rewarded " + str(gold[i]) + " gold")
    
    archer.buyBest()
    archer.buyBest()
    archer.buyBest()
    print(str(archer))
    print("Archer Damage: " + str(archer.getDamage() * 70) + " Enemy Health: " + str(health[i] * .5 - 150))
    if (archer.getDamage() * 70 < health[i] * .5 - 150):
        deathCounter += 1
        print("Died {0} times", deathCounter)
        i -= 1
    i += 1

print("Archer Damage: " + str(archer.getDamage() * 70) + " Enemy Health: " + str(health[i] * .5 - 150))
print("Died at level: " + str(i)) 

                

