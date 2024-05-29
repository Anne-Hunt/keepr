import { AppState } from "../AppState.js"
import { Keep } from "../models/Keep.js"
import { VaultKeep } from "../models/VaultKeep.js"
import { api } from "./AxiosService.js"


class VaultKeepService{
    async getVaultKeepsByVault(vaultId){
        const response = await api.get(`api/vault/${vaultId}/vaultkeeps`)
        const vaultkeeps = response.data.map(vaultkeep => new VaultKeep(vaultkeep))
        AppState.vaultkeeps = vaultkeeps
    }

    async createVaultKeep(vaultkeepData){
        vaultkeepData.creatorId = AppState.account.id
        const response = await api.post("api/vaultkeeps", vaultkeepData)
        const vaultkeep = new VaultKeep(response.data)
        AppState.vaultkeeps.push(vaultkeep)
    }

    async trashVaultKeep(vaultkeepId){
        const userId = AppState.account.id
        const vaultkeepCreator = AppState.activeVaultKeep.creatorId
        if(userId != vaultkeepCreator){
            throw new Error("You can't delete what isn't yours!")
        }
        await api.delete(`api/vaultkeeps/${vaultkeepId}`)
        const trash = AppState.vaultkeeps.findIndex(vaultkeep => vaultkeep.id = vaultkeepId)
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

    async getKeepsAndVaultKeepsByVault(vaultId){
        const response = await api.get(`api/vaultkeeps/${vaultId}/keeps`)
        const keeps = response.data.map(keepData => new Keep(keepData))
        AppState.vaultkeeps = keeps
    }

}

export const vaultKeepService = new VaultKeepService()