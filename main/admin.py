from django.contrib import admin
from .models import *
from django.utils.translation import gettext, gettext_lazy as _
from django.contrib.auth.admin import UserAdmin


@admin.register(Doctor)
class DoctorAdmin(UserAdmin):
    list_display = ['id', 'username', 'first_name', 'last_name', 'phone', 'direction', 'hospital']
    fieldsets = (
        (None, {'fields': ('username', 'password')}),
        (_('Personal info'), {'fields': ('first_name', 'last_name', 'email')}),
        (_('Permissions'), {
            'fields': ('is_active', 'is_staff', 'is_superuser', 'groups', 'user_permissions'),
        }),
        (_('Extra'), {'fields': ('phone', 'direction', 'hospital')}),
        (_('Important dates'), {'fields': ('last_login', 'date_joined')}),
    )


@admin.register(Direction)
class DirectionAdmin(admin.ModelAdmin):
    list_display = ['id', 'name']
    search_fields = ['id', 'name']


@admin.register(Province)
class ProvinceAdmin(admin.ModelAdmin):
    list_display = ['id', 'name']
    search_fields = ['id', 'name']


@admin.register(Region)
class RegionAdmin(admin.ModelAdmin):
    list_display = ['id', 'name', 'province']
    search_fields = ['id', 'name', 'province']


@admin.register(Hospital)
class HospitalAdmin(admin.ModelAdmin):
    list_display = ['id', 'name', 'phone', 'region', 'inn']
    search_fields = ['id', 'name', 'phone', 'region', 'inn']


@admin.register(Patient)
class PatientAdmin(admin.ModelAdmin):
    list_display = ['id', 'name', 'birthday']
    search_fields = ['id', 'name', 'birthday']


@admin.register(Retsepts)
class RetseptsAdmin(admin.ModelAdmin):
    list_display = ['id', 'doctor', 'patient', 'date', 'active']
    search_fields = ['id', 'doctor', 'patient', 'date', 'active']


@admin.register(Comments)
class CommentsAdmin(admin.ModelAdmin):
    list_display = ['id', 'doctor', 'patient', 'date', 'ill']
    search_fields = ['id', 'doctor', 'patient', 'date', 'ill']


@admin.register(Pharmacy)
class PharmacyAdmin(admin.ModelAdmin):
    list_display = ['id', 'username', 'login', 'password', 'region']
    search_fields = ['id', 'username', 'login', 'password', 'region']

