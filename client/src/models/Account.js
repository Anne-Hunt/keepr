export class Account {
  constructor(data) {
    this.id = data.id
    this.email = data.email
    this.name = data.name
    this.picture = data.picture
    this.coverImg = data.coverImg
    this.createdAt = data.createdAt
    this.updatedAt = data.updatedAt
  }
}
