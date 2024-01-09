import 'package:freezed_annotation/freezed_annotation.dart';

part 'permission_model.freezed.dart';
part 'permission_model.g.dart';

@freezed
class PermissionModel with _$PermissionModel{
  const factory PermissionModel({
    required int id,
    required String name,
  }) = _PermissionModel;
  
  factory PermissionModel.fromJson(Map<String,dynamic> json) => _$PermissionModelFromJson(json);
}