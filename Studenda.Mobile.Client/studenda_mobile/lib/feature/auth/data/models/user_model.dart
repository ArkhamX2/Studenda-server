import 'package:freezed_annotation/freezed_annotation.dart';
import 'package:studenda_mobile/feature/auth/data/models/role/role_model.dart';

part 'user_model.freezed.dart';
part 'user_model.g.dart';

@freezed
class UserModel with _$UserModel{
  const factory UserModel({
    @JsonKey(name: 'Id') required int id,
    @JsonKey(name: 'Role') required RoleModel role,
  }) = _UserModel;

  factory UserModel.fromJson(Map<String,dynamic> json) => _$UserModelFromJson(json);
}
