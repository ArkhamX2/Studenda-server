import 'package:freezed_annotation/freezed_annotation.dart';

part 'role_model.freezed.dart';
part 'role_model.g.dart';

@Freezed(makeCollectionsUnmodifiable: false)
class RoleModel with _$RoleModel{
  const factory RoleModel({
    @JsonKey(name: 'Id') required int id,
    @JsonKey(name: 'Name') required String name,
  }) = _RoleModel;
  
  factory RoleModel.fromJson(Map<String,dynamic> json) => _$RoleModelFromJson(json);
}
