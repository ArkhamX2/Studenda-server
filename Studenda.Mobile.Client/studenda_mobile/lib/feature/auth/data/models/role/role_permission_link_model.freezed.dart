// coverage:ignore-file
// GENERATED CODE - DO NOT MODIFY BY HAND
// ignore_for_file: type=lint
// ignore_for_file: unused_element, deprecated_member_use, deprecated_member_use_from_same_package, use_function_type_syntax_for_parameters, unnecessary_const, avoid_init_to_null, invalid_override_different_default_values_named, prefer_expression_function_bodies, annotate_overrides, invalid_annotation_target, unnecessary_question_mark

part of 'role_permission_link_model.dart';

// **************************************************************************
// FreezedGenerator
// **************************************************************************

T _$identity<T>(T value) => value;

final _privateConstructorUsedError = UnsupportedError(
    'It seems like you constructed your class using `MyClass._()`. This constructor is only meant to be used by freezed and you are not supposed to need it nor use it.\nPlease check the documentation here for more information: https://github.com/rrousselGit/freezed#custom-getters-and-methods');

RolePermissionLinkModel _$RolePermissionLinkModelFromJson(
    Map<String, dynamic> json) {
  return _RolePermisisonLinkModel.fromJson(json);
}

/// @nodoc
mixin _$RolePermissionLinkModel {
  @JsonKey(name: 'Id')
  int get id => throw _privateConstructorUsedError;
  @JsonKey(name: 'RoleId')
  int get roleId => throw _privateConstructorUsedError;
  @JsonKey(name: 'PermissionId')
  PermissionModel get permissionId => throw _privateConstructorUsedError;

  Map<String, dynamic> toJson() => throw _privateConstructorUsedError;
  @JsonKey(ignore: true)
  $RolePermissionLinkModelCopyWith<RolePermissionLinkModel> get copyWith =>
      throw _privateConstructorUsedError;
}

/// @nodoc
abstract class $RolePermissionLinkModelCopyWith<$Res> {
  factory $RolePermissionLinkModelCopyWith(RolePermissionLinkModel value,
          $Res Function(RolePermissionLinkModel) then) =
      _$RolePermissionLinkModelCopyWithImpl<$Res, RolePermissionLinkModel>;
  @useResult
  $Res call(
      {@JsonKey(name: 'Id') int id,
      @JsonKey(name: 'RoleId') int roleId,
      @JsonKey(name: 'PermissionId') PermissionModel permissionId});

  $PermissionModelCopyWith<$Res> get permissionId;
}

/// @nodoc
class _$RolePermissionLinkModelCopyWithImpl<$Res,
        $Val extends RolePermissionLinkModel>
    implements $RolePermissionLinkModelCopyWith<$Res> {
  _$RolePermissionLinkModelCopyWithImpl(this._value, this._then);

  // ignore: unused_field
  final $Val _value;
  // ignore: unused_field
  final $Res Function($Val) _then;

  @pragma('vm:prefer-inline')
  @override
  $Res call({
    Object? id = null,
    Object? roleId = null,
    Object? permissionId = null,
  }) {
    return _then(_value.copyWith(
      id: null == id
          ? _value.id
          : id // ignore: cast_nullable_to_non_nullable
              as int,
      roleId: null == roleId
          ? _value.roleId
          : roleId // ignore: cast_nullable_to_non_nullable
              as int,
      permissionId: null == permissionId
          ? _value.permissionId
          : permissionId // ignore: cast_nullable_to_non_nullable
              as PermissionModel,
    ) as $Val);
  }

  @override
  @pragma('vm:prefer-inline')
  $PermissionModelCopyWith<$Res> get permissionId {
    return $PermissionModelCopyWith<$Res>(_value.permissionId, (value) {
      return _then(_value.copyWith(permissionId: value) as $Val);
    });
  }
}

/// @nodoc
abstract class _$$RolePermisisonLinkModelImplCopyWith<$Res>
    implements $RolePermissionLinkModelCopyWith<$Res> {
  factory _$$RolePermisisonLinkModelImplCopyWith(
          _$RolePermisisonLinkModelImpl value,
          $Res Function(_$RolePermisisonLinkModelImpl) then) =
      __$$RolePermisisonLinkModelImplCopyWithImpl<$Res>;
  @override
  @useResult
  $Res call(
      {@JsonKey(name: 'Id') int id,
      @JsonKey(name: 'RoleId') int roleId,
      @JsonKey(name: 'PermissionId') PermissionModel permissionId});

  @override
  $PermissionModelCopyWith<$Res> get permissionId;
}

/// @nodoc
class __$$RolePermisisonLinkModelImplCopyWithImpl<$Res>
    extends _$RolePermissionLinkModelCopyWithImpl<$Res,
        _$RolePermisisonLinkModelImpl>
    implements _$$RolePermisisonLinkModelImplCopyWith<$Res> {
  __$$RolePermisisonLinkModelImplCopyWithImpl(
      _$RolePermisisonLinkModelImpl _value,
      $Res Function(_$RolePermisisonLinkModelImpl) _then)
      : super(_value, _then);

  @pragma('vm:prefer-inline')
  @override
  $Res call({
    Object? id = null,
    Object? roleId = null,
    Object? permissionId = null,
  }) {
    return _then(_$RolePermisisonLinkModelImpl(
      id: null == id
          ? _value.id
          : id // ignore: cast_nullable_to_non_nullable
              as int,
      roleId: null == roleId
          ? _value.roleId
          : roleId // ignore: cast_nullable_to_non_nullable
              as int,
      permissionId: null == permissionId
          ? _value.permissionId
          : permissionId // ignore: cast_nullable_to_non_nullable
              as PermissionModel,
    ));
  }
}

/// @nodoc
@JsonSerializable()
class _$RolePermisisonLinkModelImpl implements _RolePermisisonLinkModel {
  const _$RolePermisisonLinkModelImpl(
      {@JsonKey(name: 'Id') required this.id,
      @JsonKey(name: 'RoleId') required this.roleId,
      @JsonKey(name: 'PermissionId') required this.permissionId});

  factory _$RolePermisisonLinkModelImpl.fromJson(Map<String, dynamic> json) =>
      _$$RolePermisisonLinkModelImplFromJson(json);

  @override
  @JsonKey(name: 'Id')
  final int id;
  @override
  @JsonKey(name: 'RoleId')
  final int roleId;
  @override
  @JsonKey(name: 'PermissionId')
  final PermissionModel permissionId;

  @override
  String toString() {
    return 'RolePermissionLinkModel(id: $id, roleId: $roleId, permissionId: $permissionId)';
  }

  @override
  bool operator ==(Object other) {
    return identical(this, other) ||
        (other.runtimeType == runtimeType &&
            other is _$RolePermisisonLinkModelImpl &&
            (identical(other.id, id) || other.id == id) &&
            (identical(other.roleId, roleId) || other.roleId == roleId) &&
            (identical(other.permissionId, permissionId) ||
                other.permissionId == permissionId));
  }

  @JsonKey(ignore: true)
  @override
  int get hashCode => Object.hash(runtimeType, id, roleId, permissionId);

  @JsonKey(ignore: true)
  @override
  @pragma('vm:prefer-inline')
  _$$RolePermisisonLinkModelImplCopyWith<_$RolePermisisonLinkModelImpl>
      get copyWith => __$$RolePermisisonLinkModelImplCopyWithImpl<
          _$RolePermisisonLinkModelImpl>(this, _$identity);

  @override
  Map<String, dynamic> toJson() {
    return _$$RolePermisisonLinkModelImplToJson(
      this,
    );
  }
}

abstract class _RolePermisisonLinkModel implements RolePermissionLinkModel {
  const factory _RolePermisisonLinkModel(
          {@JsonKey(name: 'Id') required final int id,
          @JsonKey(name: 'RoleId') required final int roleId,
          @JsonKey(name: 'PermissionId')
          required final PermissionModel permissionId}) =
      _$RolePermisisonLinkModelImpl;

  factory _RolePermisisonLinkModel.fromJson(Map<String, dynamic> json) =
      _$RolePermisisonLinkModelImpl.fromJson;

  @override
  @JsonKey(name: 'Id')
  int get id;
  @override
  @JsonKey(name: 'RoleId')
  int get roleId;
  @override
  @JsonKey(name: 'PermissionId')
  PermissionModel get permissionId;
  @override
  @JsonKey(ignore: true)
  _$$RolePermisisonLinkModelImplCopyWith<_$RolePermisisonLinkModelImpl>
      get copyWith => throw _privateConstructorUsedError;
}
