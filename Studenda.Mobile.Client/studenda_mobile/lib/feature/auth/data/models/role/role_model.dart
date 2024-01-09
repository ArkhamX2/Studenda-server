import 'package:freezed_annotation/freezed_annotation.dart';
import 'package:studenda_mobile/feature/auth/data/models/role/role_permission_link_model.dart';

part 'role_model.freezed.dart';
part 'role_model.g.dart';

@freezed
class RoleModel with _$RoleModel{
  const factory RoleModel({
    required int id,
    required String name,
    required List<RolePermissionLinkModel> permissionLinks,
  }) = _RoleModel;
  
  factory RoleModel.fromJson(Map<String,dynamic> json) => _$RoleModelFromJson(json);
}
