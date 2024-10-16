using API.SEG.Aplicacion.Dto;
using API.SEG.Domain.Entities;
using AutoMapper;

namespace API.SEG.Transversal.Mapper
{
    public class MappingsProfile : Profile
    {
        public MappingsProfile()
        {
            #region Usuario

            CreateMap<Usuario, UsuarioDto>().ReverseMap()
            .ForMember(destination => destination.serialkey, source => source.MapFrom(src => src.serialkey))
            .ForMember(destination => destination.perfileskey, source => source.MapFrom(src => src.perfileskey))
            .ForMember(destination => destination.username, source => source.MapFrom(src => src.username))
            .ForMember(destination => destination.password, source => source.MapFrom(src => src.password))
            .ForMember(destination => destination.numdocumento, source => source.MapFrom(src => src.numdocumento))
            .ForMember(destination => destination.nombrecompleto, source => source.MapFrom(src => src.nombrecompleto))
            .ForMember(destination => destination.email, source => source.MapFrom(src => src.email))
            .ForMember(destination => destination.habilitado, source => source.MapFrom(src => src.habilitado));

            CreateMap<UsuarioDto, UsuarioIdRequest>().ReverseMap()
            .ForMember(destination => destination.serialkey, source => source.MapFrom(src => src.serialkey));

            CreateMap<UsuarioDto, UsuarioInsertRequest>().ReverseMap()
            .ForMember(destination => destination.perfileskey, source => source.MapFrom(src => src.perfileskey))
            .ForMember(destination => destination.username, source => source.MapFrom(src => src.username))
            .ForMember(destination => destination.password, source => source.MapFrom(src => src.password))
            .ForMember(destination => destination.numdocumento, source => source.MapFrom(src => src.numdocumento))
            .ForMember(destination => destination.nombrecompleto, source => source.MapFrom(src => src.nombrecompleto))
            .ForMember(destination => destination.email, source => source.MapFrom(src => src.email));

            CreateMap<UsuarioDto, UsuarioUpdateRequest>().ReverseMap()
            .ForMember(destination => destination.perfileskey, source => source.MapFrom(src => src.perfileskey))
            .ForMember(destination => destination.serialkey, source => source.MapFrom(src => src.serialkey))
            .ForMember(destination => destination.username, source => source.MapFrom(src => src.username))
            .ForMember(destination => destination.password, source => source.MapFrom(src => src.password))
            .ForMember(destination => destination.numdocumento, source => source.MapFrom(src => src.numdocumento))
            .ForMember(destination => destination.nombrecompleto, source => source.MapFrom(src => src.nombrecompleto))
            .ForMember(destination => destination.email, source => source.MapFrom(src => src.email))
            .ForMember(destination => destination.habilitado, source => source.MapFrom(src => src.habilitado));

            CreateMap<UsuarioDto, UsuarioFilterRequest>().ReverseMap()
            .ForMember(destination => destination.serialkey, source => source.MapFrom(src => src.serialkey))
            .ForMember(destination => destination.numdocumento, source => source.MapFrom(src => src.numdocumento))
            .ForMember(destination => destination.habilitado, source => source.MapFrom(src => src.habilitado))
            .ForMember(destination => destination.filtro, source => source.MapFrom(src => src.filtro));

            CreateMap<UsuarioDto, UsuarioResponse>().ReverseMap()
            .ForMember(destination => destination.serialkey, source => source.MapFrom(src => src.serialkey))
            .ForMember(destination => destination.username, source => source.MapFrom(src => src.username))
            .ForMember(destination => destination.numdocumento, source => source.MapFrom(src => src.numdocumento))
            .ForMember(destination => destination.nombrecompleto, source => source.MapFrom(src => src.nombrecompleto))
            .ForMember(destination => destination.habilitado, source => source.MapFrom(src => src.habilitado))
            .ForMember(destination => destination.email, source => source.MapFrom(src => src.email));

            #endregion

            #region Authenticate

            CreateMap<AuthenticateDto, LoginRequest>().ReverseMap()
            .ForMember(destination => destination.username, source => source.MapFrom(src => src.username))
            .ForMember(destination => destination.password, source => source.MapFrom(src => src.password));

            CreateMap<AuthenticateDto, AuthorizeProfileRequest>().ReverseMap()
            .ForMember(destination => destination.idsession, source => source.MapFrom(src => src.idsession))
            .ForMember(destination => destination.perfil_codigo, source => source.MapFrom(src => src.perfil_codigo));

            CreateMap<Usuario, AuthenticateDto>().ReverseMap()
            .ForMember(destination => destination.username, source => source.MapFrom(src => src.username))
            .ForMember(destination => destination.password, source => source.MapFrom(src => src.password));

            CreateMap<Usuario, LoginCache>().ReverseMap()
            .ForMember(destination => destination.usuario_id, source => source.MapFrom(src => src.usuario_id))
            .ForMember(destination => destination.username, source => source.MapFrom(src => src.username))
            .ForMember(destination => destination.numdocumento, source => source.MapFrom(src => src.numdocumento))
            .ForMember(destination => destination.email, source => source.MapFrom(src => src.email))
            .ForMember(destination => destination.nombrecompleto, source => source.MapFrom(src => src.nombrecompleto));

            CreateMap<Perfil, LoginPerfilesCache>().ReverseMap()
            .ForMember(destination => destination.perfil_id, source => source.MapFrom(src => src.perfil_id))
            .ForMember(destination => destination.codigo, source => source.MapFrom(src => src.codigo))
            .ForMember(destination => destination.descripcion, source => source.MapFrom(src => src.descripcion));

            CreateMap<LoginPerfilesCache, LoginPerfilesResponse>().ReverseMap()
            .ForMember(destination => destination.codigo, source => source.MapFrom(src => src.codigo))
            .ForMember(destination => destination.descripcion, source => source.MapFrom(src => src.descripcion));

            CreateMap<LoginCache, LoginResponse>().ReverseMap()
            .ForMember(destination => destination.username, source => source.MapFrom(src => src.username))
            .ForMember(destination => destination.nombrecompleto, source => source.MapFrom(src => src.nombrecompleto))
            .ForMember(destination => destination.lista_perfiles, source => source.MapFrom(src => src.lista_perfiles));

            CreateMap<UsuarioCache, AuthorizeProfileResponse>().ReverseMap()
            .ForMember(destination => destination.nombre_completo, source => source.MapFrom(src => src.nombre_completo))
            .ForMember(destination => destination.username, source => source.MapFrom(src => src.username))
            .ForMember(destination => destination.perfil_descripcion, source => source.MapFrom(src => src.perfil_descripcion))
            .ForMember(destination => destination.perfil_codigo, source => source.MapFrom(src => src.perfil_codigo))
            .ForMember(destination => destination.numdocumento, source => source.MapFrom(src => src.numdocumento));

            CreateMap<UsuarioCache, UsuarioCacheResponse>().ReverseMap()
            .ForMember(destination => destination.authkey, source => source.MapFrom(src => src.authkey))
            .ForMember(destination => destination.usuariokey, source => source.MapFrom(src => src.usuariokey))
            .ForMember(destination => destination.perfilkey, source => source.MapFrom(src => src.perfilkey))
            .ForMember(destination => destination.perfil_codigo, source => source.MapFrom(src => src.perfil_codigo))
            .ForMember(destination => destination.perfil_descripcion, source => source.MapFrom(src => src.perfil_descripcion))
            .ForMember(destination => destination.username, source => source.MapFrom(src => src.username))
            .ForMember(destination => destination.numdocumento, source => source.MapFrom(src => src.numdocumento))
            .ForMember(destination => destination.usuario_mail, source => source.MapFrom(src => src.usuario_mail))
            .ForMember(destination => destination.nombre_completo, source => source.MapFrom(src => src.nombre_completo));

            CreateMap<Accesos, AccesosResponse>().ReverseMap()
            .ForMember(destination => destination.codigo, source => source.MapFrom(src => src.codigo))
            .ForMember(destination => destination.descripcion, source => source.MapFrom(src => src.descripcion))
            .ForMember(destination => destination.abreviatura, source => source.MapFrom(src => src.abreviatura))
            .ForMember(destination => destination.url_opcion, source => source.MapFrom(src => src.url_opcion))
            .ForMember(destination => destination.icono_opcion, source => source.MapFrom(src => src.icono_opcion))
            .ForMember(destination => destination.num_orden, source => source.MapFrom(src => src.num_orden))
            .ForMember(destination => destination.lista_accesos, source => source.MapFrom(src => src.lista_accesos));

            CreateMap<AccesosDto, AccesoPermisosRequest>().ReverseMap()
           .ForMember(destination => destination.url_opcion, source => source.MapFrom(src => src.url_opcion));

            #endregion

        }
    }
}
