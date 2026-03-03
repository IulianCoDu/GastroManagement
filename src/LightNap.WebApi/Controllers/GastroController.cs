using System.Collections.Generic;
using System.Linq;
using LightNap.Core.Api;
using LightNap.Core.Data;
using LightNap.Core.Data.Entities;
using LightNap.Core.Gastro.Dto.Request;
using LightNap.Core.Gastro.Dto.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LightNap.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class GastroController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public GastroController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet("buletine-eco")]
        public async Task<ApiResponseDto<IList<BuletinEcoDto>>> GetBuletineEcoAsync()
        {
            var items = await this.context.BuletineEco
                .AsNoTracking()
                .OrderByDescending(item => item.Id)
                .Select(item => new BuletinEcoDto
                {
                    Id = item.Id,
                    Nr = item.Nr,
                    Nume = item.Nume,
                    Prenume = item.Prenume,
                    Virsta = item.Virsta,
                    Cnp = item.Cnp,
                    SeriaCs = item.SeriaCs,
                    Domiciliu = item.Domiciliu,
                    Telefon = item.Telefon,
                    Diagnostic = item.Diagnostic,
                    Ficat = item.Ficat,
                    Colecist = item.Colecist,
                    Vp = item.Vp,
                    Vs = item.Vs,
                    Cbp = item.Cbp,
                    Pancreas = item.Pancreas,
                    Splina = item.Splina,
                    Rd = item.Rd,
                    Rs = item.Rs,
                    Vu = item.Vu,
                    Prostata = item.Prostata,
                    Ogi = item.Ogi,
                    Obs = item.Obs,
                    Data = item.Data,
                    Ora = item.Ora,
                    MedicId = item.MedicId,
                    Medic = item.Medic,
                    Ceus = item.Ceus,
                    HasFig1 = item.Fig1 != null,
                    HasFig2 = item.Fig2 != null,
                    HasFig3 = item.Fig3 != null,
                    HasFilm1 = item.Film1 != null,
                })
                .ToListAsync();

            return new ApiResponseDto<IList<BuletinEcoDto>>(items);
        }

        [HttpGet("medici")]
        public async Task<ApiResponseDto<IList<string>>> GetMediciAsync()
        {
            var raw = await this.context.Medici
                .AsNoTracking()
                .ToListAsync();

            var items = raw
                .Select(medic => medic.MedicName?.Trim())
                .Where(name => !string.IsNullOrWhiteSpace(name))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(name => name)
                .ToList();

            return new ApiResponseDto<IList<string>>(items);
        }

        [HttpGet("medici-lookup")]
        public async Task<ApiResponseDto<IList<MedicLookupDto>>> GetMediciLookupAsync()
        {
            var items = await this.context.Medici
                .AsNoTracking()
                .OrderBy(medic => medic.MedicName)
                .Select(medic => new MedicLookupDto
                {
                    Id = medic.Id,
                    MedicName = medic.MedicName,
                })
                .ToListAsync();

            return new ApiResponseDto<IList<MedicLookupDto>>(items);
        }

        [HttpGet("buletine-eco/{id:int}")]
        public async Task<ApiResponseDto<BuletinEcoDto?>> GetBuletinEcoAsync(int id)
        {
            var item = await this.context.BuletineEco
                .AsNoTracking()
                .FirstOrDefaultAsync(record => record.Id == id);

            return new ApiResponseDto<BuletinEcoDto?>(item == null ? null : this.MapBuletinEco(item));
        }

        [HttpPost("buletine-eco")]
        public async Task<ApiResponseDto<BuletinEcoDto>> CreateBuletinEcoAsync([FromBody] CreateBuletinEcoDto dto)
        {
            var nextNr = (await this.context.BuletineEco.MaxAsync(record => (int?)record.Nr) ?? 0) + 1;
            var entity = new BuletinEco
            {
                Nr = nextNr,
                Nume = dto.Nume,
                Prenume = dto.Prenume,
                Virsta = dto.Virsta,
                Cnp = dto.Cnp,
                SeriaCs = dto.SeriaCs,
                Domiciliu = dto.Domiciliu,
                Telefon = dto.Telefon,
                Diagnostic = dto.Diagnostic,
                Ficat = dto.Ficat,
                Colecist = dto.Colecist,
                Vp = dto.Vp,
                Vs = dto.Vs,
                Cbp = dto.Cbp,
                Pancreas = dto.Pancreas,
                Splina = dto.Splina,
                Rd = dto.Rd,
                Rs = dto.Rs,
                Vu = dto.Vu,
                Prostata = dto.Prostata,
                Ogi = dto.Ogi,
                Obs = dto.Obs,
                Data = dto.Data,
                Ora = dto.Ora,
                MedicId = dto.MedicId,
                Medic = await this.ResolveMedicNameAsync(dto.MedicId, dto.Medic) ?? string.Empty,
                Ceus = dto.Ceus,
            };

            this.context.BuletineEco.Add(entity);
            await this.context.SaveChangesAsync();
            return new ApiResponseDto<BuletinEcoDto>(this.MapBuletinEco(entity));
        }

        [HttpPut("buletine-eco/{id:int}")]
        public async Task<ApiResponseDto<BuletinEcoDto>> UpdateBuletinEcoAsync(int id, [FromBody] UpdateBuletinEcoDto dto)
        {
            var entity = await this.context.BuletineEco.FindAsync(id);
            if (entity == null)
            {
                return new ApiResponseDto<BuletinEcoDto>
                {
                    Type = ApiResponseType.Error,
                    ErrorMessages = new[] { "Buletin ECO nu a fost gasit." },
                };
            }

            entity.Nume = dto.Nume;
            entity.Prenume = dto.Prenume;
            entity.Virsta = dto.Virsta;
            entity.Cnp = dto.Cnp;
            entity.SeriaCs = dto.SeriaCs;
            entity.Domiciliu = dto.Domiciliu;
            entity.Telefon = dto.Telefon;
            entity.Diagnostic = dto.Diagnostic;
            entity.Ficat = dto.Ficat;
            entity.Colecist = dto.Colecist;
            entity.Vp = dto.Vp;
            entity.Vs = dto.Vs;
            entity.Cbp = dto.Cbp;
            entity.Pancreas = dto.Pancreas;
            entity.Splina = dto.Splina;
            entity.Rd = dto.Rd;
            entity.Rs = dto.Rs;
            entity.Vu = dto.Vu;
            entity.Prostata = dto.Prostata;
            entity.Ogi = dto.Ogi;
            entity.Obs = dto.Obs;
            entity.Data = dto.Data;
            entity.Ora = dto.Ora;
            entity.MedicId = dto.MedicId;
            entity.Medic = await this.ResolveMedicNameAsync(dto.MedicId, dto.Medic) ?? string.Empty;
            entity.Ceus = dto.Ceus;

            await this.context.SaveChangesAsync();
            return new ApiResponseDto<BuletinEcoDto>(this.MapBuletinEco(entity));
        }

        [HttpGet("buletine-eds")]
        public async Task<ApiResponseDto<IList<BuletinEdsDto>>> GetBuletineEdsAsync()
        {
            var items = await this.context.BuletineEds
                .AsNoTracking()
                .OrderByDescending(item => item.Id)
                .Select(item => new BuletinEdsDto
                {
                    Id = item.Id,
                    Nr = item.Nr,
                    Nume = item.Nume,
                    Prenume = item.Prenume,
                    Virsta = item.Virsta,
                    Cnp = item.Cnp,
                    Domiciliu = item.Domiciliu,
                    Diagnostic = item.Diagnostic,
                    Sistem = item.Sistem,
                    SedareT = item.SedareT,
                    Medicatie = item.Medicatie,
                    Esofag = item.Esofag,
                    Jonctiune = item.Jonctiune,
                    Stomac = item.Stomac,
                    Pilor = item.Pilor,
                    Bulb = item.Bulb,
                    Duoden = item.Duoden,
                    BiopsiiL = item.BiopsiiL,
                    BiopsiiN = item.BiopsiiN,
                    Nrap = item.Nrap,
                    BiopsiiR = item.BiopsiiR,
                    Tratament = item.Tratament,
                    Data = item.Data,
                    Ora = item.Ora,
                    MedicId = item.MedicId,
                    Medic = item.Medic,
                })
                .ToListAsync();

            return new ApiResponseDto<IList<BuletinEdsDto>>(items);
        }

        [HttpGet("buletine-eds/{id:int}")]
        public async Task<ApiResponseDto<BuletinEdsDto?>> GetBuletinEdsAsync(int id)
        {
            var item = await this.context.BuletineEds
                .AsNoTracking()
                .FirstOrDefaultAsync(record => record.Id == id);

            return new ApiResponseDto<BuletinEdsDto?>(item == null ? null : this.MapBuletinEds(item));
        }

        [HttpPost("buletine-eds")]
        public async Task<ApiResponseDto<BuletinEdsDto>> CreateBuletinEdsAsync([FromBody] CreateBuletinEdsDto dto)
        {
            var nextNr = (await this.context.BuletineEds.MaxAsync(record => (int?)record.Nr) ?? 0) + 1;
            var entity = new BuletinEds
            {
                Nr = nextNr,
                Nume = dto.Nume,
                Prenume = dto.Prenume,
                Virsta = dto.Virsta,
                Cnp = dto.Cnp,
                Domiciliu = dto.Domiciliu,
                Diagnostic = dto.Diagnostic,
                Sistem = dto.Sistem,
                SedareT = dto.SedareT,
                Medicatie = dto.Medicatie,
                Esofag = dto.Esofag,
                Jonctiune = dto.Jonctiune,
                Stomac = dto.Stomac,
                Pilor = dto.Pilor,
                Bulb = dto.Bulb,
                Duoden = dto.Duoden,
                BiopsiiL = dto.BiopsiiL,
                BiopsiiN = dto.BiopsiiN,
                Nrap = dto.Nrap,
                BiopsiiR = dto.BiopsiiR,
                Tratament = dto.Tratament,
                Data = dto.Data,
                Ora = dto.Ora,
                MedicId = dto.MedicId,
                Medic = await this.ResolveMedicNameAsync(dto.MedicId, dto.Medic),
            };

            this.context.BuletineEds.Add(entity);
            await this.context.SaveChangesAsync();
            return new ApiResponseDto<BuletinEdsDto>(this.MapBuletinEds(entity));
        }

        [HttpPut("buletine-eds/{id:int}")]
        public async Task<ApiResponseDto<BuletinEdsDto>> UpdateBuletinEdsAsync(int id, [FromBody] UpdateBuletinEdsDto dto)
        {
            var entity = await this.context.BuletineEds.FindAsync(id);
            if (entity == null)
            {
                return new ApiResponseDto<BuletinEdsDto>
                {
                    Type = ApiResponseType.Error,
                    ErrorMessages = new[] { "Buletin EDS nu a fost gasit." },
                };
            }

            entity.Nume = dto.Nume;
            entity.Prenume = dto.Prenume;
            entity.Virsta = dto.Virsta;
            entity.Cnp = dto.Cnp;
            entity.Domiciliu = dto.Domiciliu;
            entity.Diagnostic = dto.Diagnostic;
            entity.Sistem = dto.Sistem;
            entity.SedareT = dto.SedareT;
            entity.Medicatie = dto.Medicatie;
            entity.Esofag = dto.Esofag;
            entity.Jonctiune = dto.Jonctiune;
            entity.Stomac = dto.Stomac;
            entity.Pilor = dto.Pilor;
            entity.Bulb = dto.Bulb;
            entity.Duoden = dto.Duoden;
            entity.BiopsiiL = dto.BiopsiiL;
            entity.BiopsiiN = dto.BiopsiiN;
            entity.Nrap = dto.Nrap;
            entity.BiopsiiR = dto.BiopsiiR;
            entity.Tratament = dto.Tratament;
            entity.Data = dto.Data;
            entity.Ora = dto.Ora;
            entity.MedicId = dto.MedicId;
            entity.Medic = await this.ResolveMedicNameAsync(dto.MedicId, dto.Medic);

            await this.context.SaveChangesAsync();
            return new ApiResponseDto<BuletinEdsDto>(this.MapBuletinEds(entity));
        }

        [HttpGet("buletine-edi")]
        public async Task<ApiResponseDto<IList<BuletinEdiDto>>> GetBuletineEdiAsync()
        {
            var items = await this.context.BuletineEdi
                .AsNoTracking()
                .OrderByDescending(item => item.Id)
                .Select(item => new BuletinEdiDto
                {
                    Id = item.Id,
                    Nr = item.Nr,
                    Nume = item.Nume,
                    Prenume = item.Prenume,
                    Virsta = item.Virsta,
                    Cnp = item.Cnp,
                    Domiciliu = item.Domiciliu,
                    Diagnostic = item.Diagnostic,
                    Sistem = item.Sistem,
                    SedareT = item.SedareT,
                    Medicatie = item.Medicatie,
                    Ileon = item.Ileon,
                    Cec = item.Cec,
                    Ascendent = item.Ascendent,
                    Transvers = item.Transvers,
                    Descendent = item.Descendent,
                    Sigmoid = item.Sigmoid,
                    Rect = item.Rect,
                    Jar = item.Jar,
                    BiopsiiL = item.BiopsiiL,
                    BiopsiiN = item.BiopsiiN,
                    Nrap = item.Nrap,
                    BiopsiiR = item.BiopsiiR,
                    Tratament = item.Tratament,
                    Data = item.Data,
                    Ora = item.Ora,
                    MedicId = item.MedicId,
                    Medic = item.Medic,
                    BiopsiiL1 = item.BiopsiiL1,
                    BiopsiiN1 = item.BiopsiiN1,
                    Nrap1 = item.Nrap1,
                    BiopsiiR1 = item.BiopsiiR1,
                    Data1 = item.Data1,
                    Ora1 = item.Ora1,
                    SedareT1 = item.SedareT1,
                    DozaS = item.DozaS,
                    Medicatie1 = item.Medicatie1,
                    DozaM = item.DozaM,
                    HasFig1 = item.Fig1 != null,
                    HasFig2 = item.Fig2 != null,
                    HasFig3 = item.Fig3 != null,
                    HasFilm1 = item.Film1 != null,
                    Consumabile = item.Consumabile,
                    Materiale = item.Materiale,
                })
                .ToListAsync();

            return new ApiResponseDto<IList<BuletinEdiDto>>(items);
        }

        [HttpGet("buletine-edi/{id:int}")]
        public async Task<ApiResponseDto<BuletinEdiDto?>> GetBuletinEdiAsync(int id)
        {
            var item = await this.context.BuletineEdi
                .AsNoTracking()
                .FirstOrDefaultAsync(record => record.Id == id);

            return new ApiResponseDto<BuletinEdiDto?>(item == null ? null : this.MapBuletinEdi(item));
        }

        [HttpPost("buletine-edi")]
        public async Task<ApiResponseDto<BuletinEdiDto>> CreateBuletinEdiAsync([FromBody] CreateBuletinEdiDto dto)
        {
            var nextNr = (await this.context.BuletineEdi.MaxAsync(record => (int?)record.Nr) ?? 0) + 1;
            var entity = new BuletinEdi
            {
                Nr = nextNr,
                Nume = dto.Nume,
                Prenume = dto.Prenume,
                Virsta = dto.Virsta,
                Cnp = dto.Cnp,
                Domiciliu = dto.Domiciliu,
                Diagnostic = dto.Diagnostic,
                Sistem = dto.Sistem,
                SedareT = dto.SedareT,
                Medicatie = dto.Medicatie,
                Ileon = dto.Ileon,
                Cec = dto.Cec,
                Ascendent = dto.Ascendent,
                Transvers = dto.Transvers,
                Descendent = dto.Descendent,
                Sigmoid = dto.Sigmoid,
                Rect = dto.Rect,
                Jar = dto.Jar,
                BiopsiiL = dto.BiopsiiL,
                BiopsiiN = dto.BiopsiiN,
                Nrap = dto.Nrap,
                BiopsiiR = dto.BiopsiiR,
                Tratament = dto.Tratament,
                Data = dto.Data,
                Ora = dto.Ora,
                MedicId = dto.MedicId,
                Medic = await this.ResolveMedicNameAsync(dto.MedicId, dto.Medic),
                BiopsiiL1 = dto.BiopsiiL1,
                BiopsiiN1 = dto.BiopsiiN1,
                Nrap1 = dto.Nrap1,
                BiopsiiR1 = dto.BiopsiiR1,
                Data1 = dto.Data1,
                Ora1 = dto.Ora1,
                SedareT1 = dto.SedareT1,
                DozaS = dto.DozaS,
                Medicatie1 = dto.Medicatie1,
                DozaM = dto.DozaM,
                Consumabile = dto.Consumabile,
                Materiale = dto.Materiale,
            };

            this.context.BuletineEdi.Add(entity);
            await this.context.SaveChangesAsync();
            return new ApiResponseDto<BuletinEdiDto>(this.MapBuletinEdi(entity));
        }

        [HttpPut("buletine-edi/{id:int}")]
        public async Task<ApiResponseDto<BuletinEdiDto>> UpdateBuletinEdiAsync(int id, [FromBody] UpdateBuletinEdiDto dto)
        {
            var entity = await this.context.BuletineEdi.FindAsync(id);
            if (entity == null)
            {
                return new ApiResponseDto<BuletinEdiDto>
                {
                    Type = ApiResponseType.Error,
                    ErrorMessages = new[] { "Buletin EDI nu a fost gasit." },
                };
            }

            entity.Nume = dto.Nume;
            entity.Prenume = dto.Prenume;
            entity.Virsta = dto.Virsta;
            entity.Cnp = dto.Cnp;
            entity.Domiciliu = dto.Domiciliu;
            entity.Diagnostic = dto.Diagnostic;
            entity.Sistem = dto.Sistem;
            entity.SedareT = dto.SedareT;
            entity.Medicatie = dto.Medicatie;
            entity.Ileon = dto.Ileon;
            entity.Cec = dto.Cec;
            entity.Ascendent = dto.Ascendent;
            entity.Transvers = dto.Transvers;
            entity.Descendent = dto.Descendent;
            entity.Sigmoid = dto.Sigmoid;
            entity.Rect = dto.Rect;
            entity.Jar = dto.Jar;
            entity.BiopsiiL = dto.BiopsiiL;
            entity.BiopsiiN = dto.BiopsiiN;
            entity.Nrap = dto.Nrap;
            entity.BiopsiiR = dto.BiopsiiR;
            entity.Tratament = dto.Tratament;
            entity.Data = dto.Data;
            entity.Ora = dto.Ora;
            entity.MedicId = dto.MedicId;
            entity.Medic = await this.ResolveMedicNameAsync(dto.MedicId, dto.Medic);
            entity.BiopsiiL1 = dto.BiopsiiL1;
            entity.BiopsiiN1 = dto.BiopsiiN1;
            entity.Nrap1 = dto.Nrap1;
            entity.BiopsiiR1 = dto.BiopsiiR1;
            entity.Data1 = dto.Data1;
            entity.Ora1 = dto.Ora1;
            entity.SedareT1 = dto.SedareT1;
            entity.DozaS = dto.DozaS;
            entity.Medicatie1 = dto.Medicatie1;
            entity.DozaM = dto.DozaM;
            entity.Consumabile = dto.Consumabile;
            entity.Materiale = dto.Materiale;

            await this.context.SaveChangesAsync();
            return new ApiResponseDto<BuletinEdiDto>(this.MapBuletinEdi(entity));
        }

        [HttpDelete("buletine-eco/{id:int}")]
        public Task<ApiResponseDto<bool>> DeleteBuletinEcoAsync(int id)
            => this.DeleteBuletineAsync(this.context.BuletineEco, id, "Buletin ECO nu a fost gasit.");

        [HttpDelete("buletine-eds/{id:int}")]
        public Task<ApiResponseDto<bool>> DeleteBuletinEdsAsync(int id)
            => this.DeleteBuletineAsync(this.context.BuletineEds, id, "Buletin EDS nu a fost gasit.");

        [HttpDelete("buletine-edi/{id:int}")]
        public Task<ApiResponseDto<bool>> DeleteBuletinEdiAsync(int id)
            => this.DeleteBuletineAsync(this.context.BuletineEdi, id, "Buletin EDI nu a fost gasit.");

        private async Task<ApiResponseDto<bool>> DeleteBuletineAsync<TEntity>(DbSet<TEntity> set, int id, string notFoundMessage)
            where TEntity : class
        {
            var item = await set.FindAsync(id);
            if (item == null)
            {
                return new ApiResponseDto<bool>
                {
                    Type = ApiResponseType.Error,
                    Result = false,
                    ErrorMessages = new[] { notFoundMessage },
                };
            }

            set.Remove(item);
            await this.context.SaveChangesAsync();
            return new ApiResponseDto<bool>(true);
        }

        private BuletinEcoDto MapBuletinEco(BuletinEco item)
            => new BuletinEcoDto
            {
                Id = item.Id,
                Nr = item.Nr,
                Nume = item.Nume,
                Prenume = item.Prenume,
                Virsta = item.Virsta,
                Cnp = item.Cnp,
                SeriaCs = item.SeriaCs,
                Domiciliu = item.Domiciliu,
                Telefon = item.Telefon,
                Diagnostic = item.Diagnostic,
                Ficat = item.Ficat,
                Colecist = item.Colecist,
                Vp = item.Vp,
                Vs = item.Vs,
                Cbp = item.Cbp,
                Pancreas = item.Pancreas,
                Splina = item.Splina,
                Rd = item.Rd,
                Rs = item.Rs,
                Vu = item.Vu,
                Prostata = item.Prostata,
                Ogi = item.Ogi,
                Obs = item.Obs,
                Data = item.Data,
                Ora = item.Ora,
                MedicId = item.MedicId,
                Medic = item.Medic,
                Ceus = item.Ceus,
                HasFig1 = item.Fig1 != null,
                HasFig2 = item.Fig2 != null,
                HasFig3 = item.Fig3 != null,
                HasFilm1 = item.Film1 != null,
            };

        private BuletinEdsDto MapBuletinEds(BuletinEds item)
            => new BuletinEdsDto
            {
                Id = item.Id,
                Nr = item.Nr,
                Nume = item.Nume,
                Prenume = item.Prenume,
                Virsta = item.Virsta,
                Cnp = item.Cnp,
                Domiciliu = item.Domiciliu,
                Diagnostic = item.Diagnostic,
                Sistem = item.Sistem,
                SedareT = item.SedareT,
                Medicatie = item.Medicatie,
                Esofag = item.Esofag,
                Jonctiune = item.Jonctiune,
                Stomac = item.Stomac,
                Pilor = item.Pilor,
                Bulb = item.Bulb,
                Duoden = item.Duoden,
                BiopsiiL = item.BiopsiiL,
                BiopsiiN = item.BiopsiiN,
                Nrap = item.Nrap,
                BiopsiiR = item.BiopsiiR,
                Tratament = item.Tratament,
                Data = item.Data,
                Ora = item.Ora,
                MedicId = item.MedicId,
                Medic = item.Medic,
            };

        private BuletinEdiDto MapBuletinEdi(BuletinEdi item)
            => new BuletinEdiDto
            {
                Id = item.Id,
                Nr = item.Nr,
                Nume = item.Nume,
                Prenume = item.Prenume,
                Virsta = item.Virsta,
                Cnp = item.Cnp,
                Domiciliu = item.Domiciliu,
                Diagnostic = item.Diagnostic,
                Sistem = item.Sistem,
                SedareT = item.SedareT,
                Medicatie = item.Medicatie,
                Ileon = item.Ileon,
                Cec = item.Cec,
                Ascendent = item.Ascendent,
                Transvers = item.Transvers,
                Descendent = item.Descendent,
                Sigmoid = item.Sigmoid,
                Rect = item.Rect,
                Jar = item.Jar,
                BiopsiiL = item.BiopsiiL,
                BiopsiiN = item.BiopsiiN,
                Nrap = item.Nrap,
                BiopsiiR = item.BiopsiiR,
                Tratament = item.Tratament,
                Data = item.Data,
                Ora = item.Ora,
                MedicId = item.MedicId,
                Medic = item.Medic,
                BiopsiiL1 = item.BiopsiiL1,
                BiopsiiN1 = item.BiopsiiN1,
                Nrap1 = item.Nrap1,
                BiopsiiR1 = item.BiopsiiR1,
                Data1 = item.Data1,
                Ora1 = item.Ora1,
                SedareT1 = item.SedareT1,
                DozaS = item.DozaS,
                Medicatie1 = item.Medicatie1,
                DozaM = item.DozaM,
                HasFig1 = item.Fig1 != null,
                HasFig2 = item.Fig2 != null,
                HasFig3 = item.Fig3 != null,
                HasFilm1 = item.Film1 != null,
                Consumabile = item.Consumabile,
                Materiale = item.Materiale,
            };

        private async Task<string?> ResolveMedicNameAsync(int? medicId, string? fallbackName)
        {
            if (!medicId.HasValue)
            {
                return fallbackName;
            }

            var medicName = await this.context.Medici
                .AsNoTracking()
                .Where(medic => medic.Id == medicId.Value)
                .Select(medic => medic.MedicName)
                .FirstOrDefaultAsync();

            return string.IsNullOrWhiteSpace(medicName) ? fallbackName : medicName;
        }
    }
}
