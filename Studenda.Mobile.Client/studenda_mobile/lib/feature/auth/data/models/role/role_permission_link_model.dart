import 'package:freezed_annotation/freezed_annotation.dart';
import 'package:studenda_mobile/feature/auth/data/models/role/permission_model.dart';

part 'role_permission_link_model.freezed.dart';
part 'role_permission_link_model.g.dart';

@freezed
class RolePermissionLinkModel with _$RolePermissionLinkModel{
  const factory RolePermissionLinkModel({
    @JsonKey(name: 'Id') required int id,
    @JsonKey(name: 'RoleId') required int roleId,
    @JsonKey(name: 'PermissionId') required PermissionModel permissionId,
  }) = _RolePermisisonLinkModel;
  
  factory RolePermissionLinkModel.fromJson(Map<String,dynamic> json) => _$RolePermissionLinkModelFromJson(json);
}
