import { AppState } from "../AppState.js"
import { Vault } from "../models/Vault.js"
import { accountService } from "./AccountService.js"
import { api } from "./AxiosService.js"


class VaultService{
  setActiveVault(vaultId) {
    const vault = AppState.vaults.find(vault => vault.id = vaultId)
    AppState.activeVault = vault
  }
  async getVaults() {
    const response = await api.get("api/vaults")
    const vaults = response.data.map(vault => new Vault(vault))
    AppState.vaults = vaults
  }

  async getVaultById(vaultId){
    const userId = AppState.account.id
    const response = await api.get(`api/vaults/${vaultId}`)
    const vault = new Vault(response.data)
    if(vault.isPrivate == true){
      await accountService.getAccount()
      if(vault.creatorId == userId){
        return AppState.activeVault = vault
      }
      if(vault.creatorId != userId){
        throw new Error("You can't access private vaults that aren't yours!")
      }
      }
    }
  

  async createVault(vaultData){
    const response = await api.post("api/vaults", vaultData)
    const vault = new Vault(response.data)
    AppState.vaults.push(vault)
  }

  async updateVault(vaultData, vaultId){
    const response = await api.put(`api/vaults/${vaultId}`, vaultData)
    const updatedVault = new Vault(response.data)
    const updated = AppState.vaults.findIndex(vault => vault.id = updatedVault.id)
    AppState.vaults.splice(updated, 1)
    AppState.vaults.push(updatedVault)
  }

  async TrashVault(vaultId){
    const userId = AppState.account.id
    const foundVault = this.getVaultById(vaultId)
    const testVault = new Vault(foundVault)
    const vaultCreator = testVault.creatorId
    if(userId != vaultCreator){
        throw new Error("You don't own this vault!")
    }
    await api.delete(`api/vaults/${vaultId}`)
    const vaultTrash = AppState.vaults.findIndex(vault => vault.id = vaultId)
    AppState.vaults.splice(vaultTrash, 1)
    AppState.activeVault = null
  }

}

export const vaultService = new VaultService()