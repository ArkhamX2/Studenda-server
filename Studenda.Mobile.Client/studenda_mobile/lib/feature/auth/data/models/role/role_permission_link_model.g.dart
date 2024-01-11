// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'role_permission_link_model.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

_$RolePermisisonLinkModelImpl _$$RolePermisisonLinkModelImplFromJson(
        Map<String, dynamic> json) =>
    _$RolePermisisonLinkModelImpl(
      id: json['Id'] as int,
      roleId: json['RoleId'] as int,
      permissionId: PermissionModel.fromJson(
          json['PermissionId'] as Map<String, dynamic>),
    );

Map<String, dynamic> _$$RolePermisisonLinkModelImplToJson(
        _$RolePermisisonLinkModelImpl instance) =>
    <String, dynamic>{
      'Id': instance.id,
      'RoleId': instance.roleId,
      'PermissionId': instance.permissionId,
    };
