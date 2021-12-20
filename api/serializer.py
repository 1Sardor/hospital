from rest_framework import serializers
from main.models import *


class DoctorSerializer(serializers.ModelSerializer):
    class Meta:
        depth = 1
        model = Doctor
        fields = '__all__'
        extra_kwargs = {'password': {'write_only': True}}


class DirectionSerializer(serializers.ModelSerializer):
    class Meta:
        model = Direction
        fields = '__all__'


class ProvinceSerializer(serializers.ModelSerializer):
    class Meta:
        model = Province
        fields = '__all__'


class RegionSerializer(serializers.ModelSerializer):
    class Meta:
        model = Region
        fields = '__all__'


class HospitalSerializer(serializers.ModelSerializer):
    class Meta:
        depth = 1
        model = Hospital
        fields = '__all__'


class PatientSerializer(serializers.ModelSerializer):
    class Meta:
        model = Patient
        fields = '__all__'


class RetseptsSerializer(serializers.ModelSerializer):
    class Meta:
        depth = 1
        model = Retsepts
        fields = '__all__'


class CommentsSerializer(serializers.ModelSerializer):
    class Meta:
        depth = 1
        model = Comments
        fields = '__all__'


class PharmacySerializer(serializers.ModelSerializer):
    class Meta:
        depth = 1
        model = Pharmacy
        fields = '__all__'