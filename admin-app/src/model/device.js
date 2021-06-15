export class device {
  constructor(id, name, hasLight, hasTemp, hasHum, hasPH, isNew) {
    this.id = id;
    this.name = name;
    this.hasLight = hasLight;
    this.hasTemp = hasTemp;
    this.hasHum = hasHum;
    this.hasPH = hasPH;
    this.isNew = isNew;
  }
}