import 'package:freezed_annotation/freezed_annotation.dart';

part 'permission_model.freezed.dart';
part 'permission_model.g.dart';

@freezed
class PermissionModel with _$PermissionModel{
  const factory PermissionModel({
    @JsonKey(name: 'Id') required int id,
    @JsonKey(name: 'Name') required String name,
  }) = _PermissionModel;
  
  factory PermissionModel.fromJson(Map<String,dynamic> json) => _$PermissionModelFromJson(json);
}
