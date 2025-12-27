import { HttpClient } from "@angular/common/http";
import { Injectable, inject } from "@angular/core";
import {
  BuletinEcoDto,
  BuletinEdsDto,
  BuletinEdiDto,
  CreateBuletinEcoDto,
  CreateBuletinEdsDto,
  CreateBuletinEdiDto,
  UpdateBuletinEcoDto,
  UpdateBuletinEdsDto,
  UpdateBuletinEdiDto,
} from "../dtos";

@Injectable({
  providedIn: "root",
})
export class GastroDataService {
  #http = inject(HttpClient);
  #apiUrlRoot = "/api/gastro/";

  getBuletineEco() {
    return this.#http.get<Array<BuletinEcoDto>>(`${this.#apiUrlRoot}buletine-eco`);
  }

  getBuletinEco(id: number) {
    return this.#http.get<BuletinEcoDto | null>(`${this.#apiUrlRoot}buletine-eco/${id}`);
  }

  getBuletineEds() {
    return this.#http.get<Array<BuletinEdsDto>>(`${this.#apiUrlRoot}buletine-eds`);
  }

  getBuletinEds(id: number) {
    return this.#http.get<BuletinEdsDto | null>(`${this.#apiUrlRoot}buletine-eds/${id}`);
  }

  getBuletineEdi() {
    return this.#http.get<Array<BuletinEdiDto>>(`${this.#apiUrlRoot}buletine-edi`);
  }

  getBuletinEdi(id: number) {
    return this.#http.get<BuletinEdiDto | null>(`${this.#apiUrlRoot}buletine-edi/${id}`);
  }

  getMedici() {
    return this.#http.get<Array<string>>(`${this.#apiUrlRoot}medici`);
  }

  createBuletinEco(dto: CreateBuletinEcoDto) {
    return this.#http.post<BuletinEcoDto>(`${this.#apiUrlRoot}buletine-eco`, dto);
  }

  updateBuletinEco(id: number, dto: UpdateBuletinEcoDto) {
    return this.#http.put<BuletinEcoDto>(`${this.#apiUrlRoot}buletine-eco/${id}`, dto);
  }

  createBuletinEds(dto: CreateBuletinEdsDto) {
    return this.#http.post<BuletinEdsDto>(`${this.#apiUrlRoot}buletine-eds`, dto);
  }

  updateBuletinEds(id: number, dto: UpdateBuletinEdsDto) {
    return this.#http.put<BuletinEdsDto>(`${this.#apiUrlRoot}buletine-eds/${id}`, dto);
  }

  createBuletinEdi(dto: CreateBuletinEdiDto) {
    return this.#http.post<BuletinEdiDto>(`${this.#apiUrlRoot}buletine-edi`, dto);
  }

  updateBuletinEdi(id: number, dto: UpdateBuletinEdiDto) {
    return this.#http.put<BuletinEdiDto>(`${this.#apiUrlRoot}buletine-edi/${id}`, dto);
  }

  deleteBuletineEco(id: number) {
    return this.#http.delete<boolean>(`${this.#apiUrlRoot}buletine-eco/${id}`);
  }

  deleteBuletineEds(id: number) {
    return this.#http.delete<boolean>(`${this.#apiUrlRoot}buletine-eds/${id}`);
  }

  deleteBuletineEdi(id: number) {
    return this.#http.delete<boolean>(`${this.#apiUrlRoot}buletine-edi/${id}`);
  }
}
