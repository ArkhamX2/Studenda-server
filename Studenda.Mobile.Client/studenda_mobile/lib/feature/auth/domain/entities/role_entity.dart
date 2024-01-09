import 'package:freezed_annotation/freezed_annotation.dart';

part 'role_entity.freezed.dart';

@freezed
class RoleEntity with _$RoleEntity{
  const factory RoleEntity({
    required int id,
    required String name,
  }) = _RoleEntity;

}

