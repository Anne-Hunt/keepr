import { AppState } from "../AppState.js"
import { VaultKeep } from "../models/VaultKeep.js"
import { api } from "./AxiosService.js"


class VaultKeepService{
    async getVaultKeepsByVault(vaultId){
        const response = await api.get(`api/vault/${vaultId}/vaultkeeps`)
        const vaultkeeps = response.data.map(vaultkeep => new VaultKeep(vaultkeep))
        AppState.keepsInVault.push(vaultkeeps)
    }

    async createVaultKeep(vaultkeepData){
        vaultkeepData.creatorId = AppState.account.id
        const response = await api.post("api/vaultkeeps", vaultkeepData)
        const vaultkeep = new VaultKeep(response.data)
        AppState.vaultkeeps.push(vaultkeep)
    }

    async trashVaultKeep(vaultkeepId){
        await api.delete(`api/vaultkeeps/${vaultkeepId}`)
        const trash = AppState.keepsInVault.findIndex(vaultkeep => vaultkeep.vaultKeepId = vaultkeepId)
        AppState.vaultkeeps.splice(trash, 1)
    }

    async getVaultKeeps(){
        const response = await api.get("api/vaultkeeps")
        const vaultkeeps = response.data.map(vaultkeep => new VaultKeep(vaultkeep))
        AppState.vaultkeeps = vaultkeeps
    }

    async getVaultKeepById(vaultkeepId){
        const response = await api.get(`api/vaultkeeps/${vaultkeepId}`)
        const vaultkeep = new VaultKeep(response.data)
        AppState.activeVaultKeep = vaultkeep
    }

}

export const vaultKeepService = new VaultKeepService()