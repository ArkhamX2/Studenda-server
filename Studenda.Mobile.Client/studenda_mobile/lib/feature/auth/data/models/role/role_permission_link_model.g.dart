// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'role_permission_link_model.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

_$RolePermisisonLinkModelImpl _$$RolePermisisonLinkModelImplFromJson(
        Map<String, dynamic> json) =>
    _$RolePermisisonLinkModelImpl(
      id: json['id'] as int,
      roleId: json['roleId'] as int,
      permissionId: PermissionModel.fromJson(
          json['permissionId'] as Map<String, dynamic>),
    );

Map<String, dynamic> _$$RolePermisisonLinkModelImplToJson(
        _$RolePermisisonLinkModelImpl instance) =>
    <String, dynamic>{
      'id': instance.id,
      'roleId': instance.roleId,
      'permissionId': instance.permissionId,
    };
