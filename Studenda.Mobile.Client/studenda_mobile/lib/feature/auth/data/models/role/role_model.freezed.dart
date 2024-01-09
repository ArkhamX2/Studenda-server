// coverage:ignore-file
// GENERATED CODE - DO NOT MODIFY BY HAND
// ignore_for_file: type=lint
// ignore_for_file: unused_element, deprecated_member_use, deprecated_member_use_from_same_package, use_function_type_syntax_for_parameters, unnecessary_const, avoid_init_to_null, invalid_override_different_default_values_named, prefer_expression_function_bodies, annotate_overrides, invalid_annotation_target, unnecessary_question_mark

part of 'role_model.dart';

// **************************************************************************
// FreezedGenerator
// **************************************************************************

T _$identity<T>(T value) => value;

final _privateConstructorUsedError = UnsupportedError(
    'It seems like you constructed your class using `MyClass._()`. This constructor is only meant to be used by freezed and you are not supposed to need it nor use it.\nPlease check the documentation here for more information: https://github.com/rrousselGit/freezed#custom-getters-and-methods');

RoleModel _$RoleModelFromJson(Map<String, dynamic> json) {
  return _RoleModel.fromJson(json);
}

/// @nodoc
mixin _$RoleModel {
  int get id => throw _privateConstructorUsedError;
  String get name => throw _privateConstructorUsedError;
  List<RolePermissionLinkModel> get permissionLinks =>
      throw _privateConstructorUsedError;

  Map<String, dynamic> toJson() => throw _privateConstructorUsedError;
  @JsonKey(ignore: true)
  $RoleModelCopyWith<RoleModel> get copyWith =>
      throw _privateConstructorUsedError;
}

/// @nodoc
abstract class $RoleModelCopyWith<$Res> {
  factory $RoleModelCopyWith(RoleModel value, $Res Function(RoleModel) then) =
      _$RoleModelCopyWithImpl<$Res, RoleModel>;
  @useResult
  $Res call(
      {int id, String name, List<RolePermissionLinkModel> permissionLinks});
}

/// @nodoc
class _$RoleModelCopyWithImpl<$Res, $Val extends RoleModel>
    implements $RoleModelCopyWith<$Res> {
  _$RoleModelCopyWithImpl(this._value, this._then);

  // ignore: unused_field
  final $Val _value;
  // ignore: unused_field
  final $Res Function($Val) _then;

  @pragma('vm:prefer-inline')
  @override
  $Res call({
    Object? id = null,
    Object? name = null,
    Object? permissionLinks = null,
  }) {
    return _then(_value.copyWith(
      id: null == id
          ? _value.id
          : id // ignore: cast_nullable_to_non_nullable
              as int,
      name: null == name
          ? _value.name
          : name // ignore: cast_nullable_to_non_nullable
              as String,
      permissionLinks: null == permissionLinks
          ? _value.permissionLinks
          : permissionLinks // ignore: cast_nullable_to_non_nullable
              as List<RolePermissionLinkModel>,
    ) as $Val);
  }
}

/// @nodoc
abstract class _$$RoleModelImplCopyWith<$Res>
    implements $RoleModelCopyWith<$Res> {
  factory _$$RoleModelImplCopyWith(
          _$RoleModelImpl value, $Res Function(_$RoleModelImpl) then) =
      __$$RoleModelImplCopyWithImpl<$Res>;
  @override
  @useResult
  $Res call(
      {int id, String name, List<RolePermissionLinkModel> permissionLinks});
}

/// @nodoc
class __$$RoleModelImplCopyWithImpl<$Res>
    extends _$RoleModelCopyWithImpl<$Res, _$RoleModelImpl>
    implements _$$RoleModelImplCopyWith<$Res> {
  __$$RoleModelImplCopyWithImpl(
      _$RoleModelImpl _value, $Res Function(_$RoleModelImpl) _then)
      : super(_value, _then);

  @pragma('vm:prefer-inline')
  @override
  $Res call({
    Object? id = null,
    Object? name = null,
    Object? permissionLinks = null,
  }) {
    return _then(_$RoleModelImpl(
      id: null == id
          ? _value.id
          : id // ignore: cast_nullable_to_non_nullable
              as int,
      name: null == name
          ? _value.name
          : name // ignore: cast_nullable_to_non_nullable
              as String,
      permissionLinks: null == permissionLinks
          ? _value._permissionLinks
          : permissionLinks // ignore: cast_nullable_to_non_nullable
              as List<RolePermissionLinkModel>,
    ));
  }
}

/// @nodoc
@JsonSerializable()
class _$RoleModelImpl implements _RoleModel {
  const _$RoleModelImpl(
      {required this.id,
      required this.name,
      required final List<RolePermissionLinkModel> permissionLinks})
      : _permissionLinks = permissionLinks;

  factory _$RoleModelImpl.fromJson(Map<String, dynamic> json) =>
      _$$RoleModelImplFromJson(json);

  @override
  final int id;
  @override
  final String name;
  final List<RolePermissionLinkModel> _permissionLinks;
  @override
  List<RolePermissionLinkModel> get permissionLinks {
    if (_permissionLinks is EqualUnmodifiableListView) return _permissionLinks;
    // ignore: implicit_dynamic_type
    return EqualUnmodifiableListView(_permissionLinks);
  }

  @override
  String toString() {
    return 'RoleModel(id: $id, name: $name, permissionLinks: $permissionLinks)';
  }

  @override
  bool operator ==(Object other) {
    return identical(this, other) ||
        (other.runtimeType == runtimeType &&
            other is _$RoleModelImpl &&
            (identical(other.id, id) || other.id == id) &&
            (identical(other.name, name) || other.name == name) &&
            const DeepCollectionEquality()
                .equals(other._permissionLinks, _permissionLinks));
  }

  @JsonKey(ignore: true)
  @override
  int get hashCode => Object.hash(runtimeType, id, name,
      const DeepCollectionEquality().hash(_permissionLinks));

  @JsonKey(ignore: true)
  @override
  @pragma('vm:prefer-inline')
  _$$RoleModelImplCopyWith<_$RoleModelImpl> get copyWith =>
      __$$RoleModelImplCopyWithImpl<_$RoleModelImpl>(this, _$identity);

  @override
  Map<String, dynamic> toJson() {
    return _$$RoleModelImplToJson(
      this,
    );
  }
}

abstract class _RoleModel implements RoleModel {
  const factory _RoleModel(
          {required final int id,
          required final String name,
          required final List<RolePermissionLinkModel> permissionLinks}) =
      _$RoleModelImpl;

  factory _RoleModel.fromJson(Map<String, dynamic> json) =
      _$RoleModelImpl.fromJson;

  @override
  int get id;
  @override
  String get name;
  @override
  List<RolePermissionLinkModel> get permissionLinks;
  @override
  @JsonKey(ignore: true)
  _$$RoleModelImplCopyWith<_$RoleModelImpl> get copyWith =>
      throw _privateConstructorUsedError;
}
