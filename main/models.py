from django.db import models
from django.contrib.auth.models import AbstractUser


class Direction(models.Model):
    name = models.CharField(max_length=255)

    def __str__(self):
        return self.name


class Province(models.Model):
    name = models.CharField(max_length=255)

    def __str__(self):
        return self.name


class Region(models.Model):
    province = models.ForeignKey(Province, on_delete=models.CASCADE)
    name = models.CharField(max_length=255)

    def __str__(self):
        return self.province.name + ' + ' + self.name


class Hospital(models.Model):
    name = models.CharField(max_length=255, unique=True)
    phone = models.CharField(max_length=255)
    region = models.ForeignKey(Region, on_delete=models.CASCADE)
    inn = models.CharField(max_length=255)

    def __str__(self):
        return self.name


class Doctor(AbstractUser):
    phone = models.CharField(max_length=25, null=True, blank=True)
    direction = models.ForeignKey(Direction, on_delete=models.CASCADE, null=True, blank=True)
    hospital = models.ForeignKey(Hospital, on_delete=models.CASCADE, null=True, blank=True)

    def __str__(self):
        return self.username

    class Meta(AbstractUser.Meta):
        swappable = 'AUTH_USER_MODEL'
        verbose_name = 'Doctors'
        verbose_name_plural = 'Doctors'


class Patient(models.Model):
    name = models.CharField(max_length=255)
    birthday = models.DateField()

    def __str__(self):
        return self.name


class Retsepts(models.Model):
    doctor = models.ForeignKey(Doctor, on_delete=models.CASCADE)
    patient = models.ForeignKey(Patient, on_delete=models.CASCADE)
    date = models.DateTimeField(auto_now_add=True)
    active = models.BooleanField(default=False)

    def __str__(self):
        return self.patient.name


class Comments(models.Model):
    patient = models.ForeignKey(Patient, on_delete=models.CASCADE)
    doctor = models.ForeignKey(Doctor, on_delete=models.CASCADE)
    ill = models.CharField(max_length=255)
    date = models.DateTimeField(auto_now_add=True)

    def __str__(self):
        return self.patient.name


class Pharmacy(models.Model):
    name = models.CharField(max_length=255)
    login = models.CharField(max_length=255, unique=True)
    password = models.CharField(max_length=255)
    region = models.ForeignKey(Region, on_delete=models.CASCADE)

    def __str__(self):
        return self.name
