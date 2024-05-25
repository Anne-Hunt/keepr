import { AppState } from "../AppState.js"
import { Vault } from "../models/Vault.js"
import { api } from "./AxiosService.js"


class VaultService{
  async getVaults() {
    const response = await api.get("api/vaults")
    const vaults = response.data.map(vault => new Vault(vault))
    AppState.vaults = vaults
  }

}

export const vaultService = new VaultService()