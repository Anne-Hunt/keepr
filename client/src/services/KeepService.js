import { AppState } from "../AppState.js"
import { Keep } from "../models/Keep.js"
import { api } from "./AxiosService.js"

class KeepService{
    async getKeepsByVault(vaultId) {
        const response = await api.get(`api/vaults/${vaultId}/keeps`)
        const keeps = response.data.map(keep => new Keep(keep))
        AppState.keeps = keeps
    }

}

export const keepService = new KeepService()